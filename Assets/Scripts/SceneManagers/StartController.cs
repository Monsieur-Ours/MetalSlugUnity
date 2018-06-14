using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<AudioManager>().Play("Menu");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("Metal_Slug");
        }
	}

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
