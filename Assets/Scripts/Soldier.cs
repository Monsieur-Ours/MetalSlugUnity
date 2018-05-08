using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : EnemyController {

    private float fireRate = 1f;
    private float nextFire = 0f;
    private Animator anim;

    public float meleeDistance;
    public GameObject bulletPrefab;
    public Transform gunTip;

    private bool isDead = false;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        if(!isDead)
        {
            if(target)
            {
                base.Update();

                if (Mathf.Abs(targetDistance) < viewDistance && Mathf.Abs(targetDistance) > attackDistance && Mathf.Abs(targetDistance) > meleeDistance)
                {
                    anim.SetFloat("Speed", speed);
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                }
                else if(Mathf.Abs(targetDistance) <= attackDistance && Time.time > nextFire && Mathf.Abs(targetDistance) > meleeDistance)
                {
                    nextFire = Time.time + fireRate;
                    anim.SetFloat("Speed", 0);
                    anim.SetTrigger("Shoot");

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
                else if (Mathf.Abs(targetDistance) <= meleeDistance) 
                {
                    anim.SetTrigger("Melee");
                }
                else
                {
                    anim.SetFloat("Speed", 0);
                }
            }
        }
        else
        {
            anim.SetTrigger("Death");
            Debug.Log("Enemy dead");
        }
	}

    void Dead(int health)
    {
        if (health <= 0)
        {
            Debug.Log("Enemy Health at 0");
            isDead = true;
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
