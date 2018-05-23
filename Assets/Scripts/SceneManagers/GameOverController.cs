using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { 
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("Main Menu");
            }
    }
}
