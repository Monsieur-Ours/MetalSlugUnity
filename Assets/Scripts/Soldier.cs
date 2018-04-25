using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : EnemyController {

    private float fireRate = 0.5f;
    private float nextFire = 0f;
    public GameObject bulletPrefab;
    public Transform gunTip;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        if (Mathf.Abs(targetDistance) < viewDistance && Mathf.Abs(targetDistance) > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else if(targetDistance <= attackDistance && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
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

    void Dead(int health)
    {
        if (health <= 0)
        {
            Debug.Log("Enemy Dead");
            gameObject.SetActive(false);
        }
    }
}
