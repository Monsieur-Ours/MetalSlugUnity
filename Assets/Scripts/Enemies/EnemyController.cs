using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    public float attackDistance;
    public float viewDistance;
   
    //protected Animator anim;
    protected bool facingRight = false;
    protected Transform target;
    protected float targetDistance;
    protected Rigidbody2D rb2d;
    protected SpriteRenderer sprite;

    // Use this for initialization
    void Awake () {
        //anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (target)
        {
            targetDistance = transform.position.x - target.position.x;        
        }
    }

    virtual protected void FixedUpdate()
    {
        if (target)
        {
            if (transform.position.x > target.position.x && facingRight)
            {
                Flip();
            }
            else if (transform.position.x < target.position.x && !facingRight)
            {
                Flip();
            }
        }
    }

    protected void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
