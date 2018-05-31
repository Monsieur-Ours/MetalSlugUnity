using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinningScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<AudioManager>().Play("Mission Complete");

        FindObjectOfType<AudioManager>().Play("Menu");

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
