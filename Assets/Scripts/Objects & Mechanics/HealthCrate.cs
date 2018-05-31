using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrate : MonoBehaviour {

    public int healthAmount;
    public AudioManager audioManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SendMessage("GainHealth", healthAmount, SendMessageOptions.DontRequireReceiver);
        Debug.Log("Healt Crate detected" + healthAmount.ToString());
        audioManager.Play("Ok");
        AfterTriggerEnter();

    }

    private void AfterTriggerEnter()
    {
        Destroy(gameObject);
    }
}
