using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float jumpForce = 600;
    public GameObject bulletPrefab;
    public Transform gunTip;

    private Animator anim;
    private Rigidbody2D rb2d;
    private bool facingRight = true;
    private bool jump;
    private bool onGround = false;
    private Transform groundCheck;
    private float hForce = 0;
    private bool crouched;
    private bool lookingUp;
    private bool reloading;
    private float fireRate = 0.1f;
    private float nextFire = 0f;

    private bool isDead = false;


	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(!isDead)
        {
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            if(onGround)
            {
                anim.SetBool("Jump", false);
            }

            if(Input.GetButtonDown("Jump") && onGround && !reloading)
            {
                jump = true;
            } else if (Input.GetButtonUp("Jump"))
            {
                if(rb2d.velocity.y > 0)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
                }
            }

            if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                anim.SetTrigger("Shoot");
                GameObject tempBullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
                if(!facingRight && !lookingUp)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0, 0, 180f);
                }
                else if (!facingRight && lookingUp)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0, 0, 90f);
                }
                if (crouched && !onGround)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0, 0, -90f);
                }
            }

            if (Input.GetButtonDown("Melee"))
            {
                anim.SetTrigger("Melee");
            }

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                lookingUp = true;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                crouched = true;
            }
            else
            {
                lookingUp = false;
                crouched = false;
            }

            anim.SetBool("LookingUp", lookingUp);
            anim.SetBool("Crouched", crouched);
            if(Input.GetButtonDown("Reload"))
            {
                reloading = true;
                anim.SetBool("Reloading", true);
            }

            if((crouched || lookingUp || reloading) && onGround)
            {
                hForce = 0;
            }

        } else
        {
            anim.SetTrigger("Death");
            Debug.Log("You're dead");
        }
	}

    private void FixedUpdate()
    {
        if(!isDead)
        {
            if(!crouched && !lookingUp && !reloading)
            hForce = Input.GetAxisRaw("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(hForce));

            rb2d.velocity = new Vector2(hForce * speed, rb2d.velocity.y);

            if(hForce > 0 && !facingRight)
            {
                Flip();
            }
            else if (hForce < 0 && facingRight)
            {
                Flip();
            }

            if(jump)
            {
                anim.SetBool("Jump", true);
                jump = false;
                rb2d.AddForce(Vector2.up * jumpForce);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Dead(int health)
    {
        if(health <= 0)
        {
            Debug.Log("Player Health at 0");
            isDead = true;
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
