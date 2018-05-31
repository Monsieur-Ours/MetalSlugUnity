using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /* 
     * ----------------------------
     * --------- Publics ----------
     * ----------------------------
     */
    public float        speed     = 5f;        // Player Speed
    public float        jumpForce = 600;       // Player jump force
                        
    public GameObject   bulletPrefab;          // Bullet prefab instantiated at fire
    public Transform    gunTip;                // Gun tip ( Where the bullet shoot from)
    public int          chargerCapacity = 20;  // Charger capacity
                        
    public GameObject   grenadePrefab;         // Grenade prefab instantiated on throwing grenade
    public Transform    handGrip;              // Position of the hand to instantiate grenade at this position
    public int          grenadeCount = 10;      // Number of grenades in pocket

    public Text chargerText;                   // Charger capacity text
    public Text grenadeText;                   // Grenade possesed text
    public AudioManager audioManager;          // Audio Manager

    /* 
     * -----------------------------
     * --------- Privates ----------
     * -----------------------------
     */

    private Animator    anim;                  // Player animator
    private Rigidbody2D rb2d;                  // Player rigide body 2D
    private bool        facingRight = true;    // Is player facing right ?
    private bool        jump;                  // Is player jumping ?
    private bool        onGround = false;      // Is on the ground ?
    private Transform   groundCheck;           // Position of the ground checker
    private float       hForce = 0;            // Horizontal force used to create movement
    private bool        crouched;              // Is player is crouched ?
    private bool        lookingUp;             // Is player is looking up ?
    private bool        reloading;             // Is layer is reloading ?

    private float       fireRate = 0.1f;       // Fire rate before shooting or throwing a grenade
    private float       nextFire = 0f;         // Time before next shoot
    private float       grenadeRate = 0.5f;    // Time before next grenade

    private bool        isDead = false;        // Is player dead ?


	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        chargerText.text = chargerCapacity.ToString();
        grenadeText.text = grenadeCount.ToString();

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(!isDead)
        {
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            if (onGround)
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

            if (Input.GetButtonDown("Fire1") && Time.time > nextFire && chargerCapacity > 0)
            {
                chargerCapacity--;
                chargerText.text = chargerCapacity.ToString();
                nextFire = Time.time + fireRate;
                anim.SetTrigger("Shoot");
                audioManager.Play("Shoot");
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
                audioManager.Play("Melee");
            }

            if(Input.GetButtonDown("Throw Grenade") && Time.time > nextFire && grenadeCount > 0)
            {
                grenadeCount--;
                grenadeText.text = grenadeCount.ToString();
                nextFire = Time.time + grenadeRate;
                anim.SetTrigger("Grenade");
                audioManager.Play("Melee");
                GameObject tempGrenade = Instantiate(grenadePrefab, handGrip.position, handGrip.rotation);
                if (!facingRight && !lookingUp)
                {
                    tempGrenade.transform.eulerAngles = new Vector3(0, 0, 180f);
                }
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
            if(Input.GetButtonDown("Reload") && chargerCapacity < 10)
            {
                chargerCapacity = 10;
                chargerText.text = chargerCapacity.ToString();
                reloading = true;
                anim.SetBool("Reloading", true);
            }

            if((crouched || lookingUp || reloading) && onGround)
            {
                hForce = 0;
            }

        } 
	}

    private void FixedUpdate()
    {
        if(!isDead)
        {
            anim.SetBool("Reloading", false);
            reloading = false;

            if (!crouched && !lookingUp && !reloading)
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
            anim.SetTrigger("Death");
        }
    }

    void GainGrenade(int grenadeAmount)
    {
        grenadeCount += grenadeAmount;
        grenadeText.text = grenadeCount.ToString();
    }

    void DestroyObject()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }
}
