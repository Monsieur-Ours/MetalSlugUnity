using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : EnemyController {

    // Shooting speed
    private float fireRate = 1f;
    private float nextFire = 0f;
   
    // Throwing grenades speed
    private float nextGrenade = 0f;
    private float grenadeRate = 50f;

    private Animator anim;

    public float meleeDistance;

    public GameObject bulletPrefab;
    public Transform gunTip;

    public GameObject grenadePrefab;
    public Transform handGrip;

    public AudioManager audioManager;

    private bool isDead = false;
    private bool isThrowingGrenade = false;

    private GameController gc;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        gc = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        if(!isDead)
        {
            if(target)
            {
                base.Update();

                // If at attack distance but not melee, shoot or throw a grenade
                if (Mathf.Abs(targetDistance) < viewDistance && Mathf.Abs(targetDistance) > attackDistance && Mathf.Abs(targetDistance) > meleeDistance)
                {
                    anim.SetFloat("Speed", speed);
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                    if ( Time.time > nextFire && !isThrowingGrenade)
                    {
                        if (Time.time > nextGrenade)
                        {
                            isThrowingGrenade = true;
                            audioManager.Play("Melee");
                            nextGrenade = Time.time + grenadeRate;
                            if (facingRight)
                            {
                                GameObject tempGrenade = Instantiate(grenadePrefab, handGrip.position, handGrip.rotation);
                                isThrowingGrenade = false;
                            }
                            else if (!facingRight)
                            {
                                GameObject tempGrenade = Instantiate(grenadePrefab, handGrip.position, handGrip.rotation);
                                tempGrenade.transform.eulerAngles = new Vector3(0, 0, 180f);
                                isThrowingGrenade = false;
                            }
                        }
                        else
                        {
                            nextFire = Time.time + fireRate;
                            anim.SetFloat("Speed", 0);
                            anim.SetTrigger("Shoot");
                            audioManager.Play("Shoot");

                            if (facingRight)
                            {
                                GameObject tempBullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
                            }
                            else if (!facingRight)
                            {
                                GameObject tempBullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
                                tempBullet.transform.eulerAngles = new Vector3(0, 0, 180f);
                            }
                        }
                    }
                }
                else if (Mathf.Abs(targetDistance) <= meleeDistance)
                {
                    anim.SetTrigger("Melee");
                    audioManager.Play("Melee");
                }
                else
                {
                    anim.SetFloat("Speed", 0);
                }
            }
        }
	}

    void Dead(int health)
    {
        if (health <= 0)
        {
            Debug.Log("Enemy Health at 0");
            isDead = true;
            anim.SetTrigger("Death");
            gc.SendMessage("IncreaseScore", 100f, SendMessageOptions.DontRequireReceiver);
            Debug.Log("Enemy dead");
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
