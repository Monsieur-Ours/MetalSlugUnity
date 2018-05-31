using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : DamageEffector {

    public float speed = 5f;
    public float destroyTime = 1.5f;

    // Use this for initialization
    void Start()
    {
        damage = 5;
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    protected override void AfterTriggerEnter()
    {
        Destroy(gameObject);
    }
}
