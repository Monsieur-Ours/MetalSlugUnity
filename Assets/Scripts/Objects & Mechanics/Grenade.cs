using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : DamageEffector {
    public float speed = 20f;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    protected override void AfterTriggerEnter()
    {
        speed = 0f;
        FindObjectOfType<AudioManager>().Play("Explosion");
        anim.SetTrigger("Explode");
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
