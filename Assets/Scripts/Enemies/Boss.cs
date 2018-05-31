using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Boss : EnemyController {

    // Shooting speed
    private float fireRate = 3f;
    private float nextFire = 0f;

    private Animator anim;

    public GameObject fireBallPrefab;
    public Transform canonTip;

    private bool isDead = false;

    private GameController gc;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!isDead)
        {
            if (target)
            {
                base.Update();
                
                // If at attack distance
                if (Mathf.Abs(targetDistance) <= attackDistance && Time.time > nextFire)
                { 
                    nextFire = Time.time + fireRate;
                    anim.SetTrigger("Shoot");
                    FindObjectOfType<AudioManager>().Play("Laser");
                    GameObject fireBall = Instantiate(fireBallPrefab, canonTip.position, canonTip.rotation);
                    GameObject fireBall2 = Instantiate(fireBallPrefab, canonTip.position, canonTip.rotation);
                    GameObject fireBall3 = Instantiate(fireBallPrefab, canonTip.position, canonTip.rotation);
                    GameObject fireBall4 = Instantiate(fireBallPrefab, canonTip.position, canonTip.rotation);
                    fireBall.transform.eulerAngles = new Vector3(0, 0, 180f);
                    fireBall2.transform.eulerAngles = new Vector3(0, 0, 160f);
                    fireBall3.transform.eulerAngles = new Vector3(0, 0, 140f);
                    fireBall4.transform.eulerAngles = new Vector3(0, 0, 120f);
                }
            }
        }
    }

    void Dead(int health)
    {
        if (health <= 0)
        {
            Debug.Log("Boss Health at 0");
            isDead = true;
            gc.SendMessage("IncreaseScore", 10000f, SendMessageOptions.DontRequireReceiver);
            SceneManager.LoadScene("WinScene");
            Debug.Log("Enemy dead");
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }


    protected override void FixedUpdate()
    {

    }
}
