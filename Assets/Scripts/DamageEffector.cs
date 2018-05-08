using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffector : MonoBehaviour {
    public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SendMessage("TakeDamageEffect", this, SendMessageOptions.DontRequireReceiver);
        Debug.Log("Trigger detected");
        AfterTriggerEnter();

    }

    virtual protected void AfterTriggerEnter()
    {

    }
}
