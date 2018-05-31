using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Text timerText;
    public Text scoreText;

    private float timer = 100f;
    private float score = 0f;

    private void Awake()
    {
        FindObjectOfType<AudioManager>().Play("Stage 1");
        FindObjectOfType<AudioManager>().Play("Mission Start");
    }

    // Use this for initialization
    void Start()
    {
        timerText.text = Mathf.Round(timer).ToString();
        scoreText.text = "Score : " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = Mathf.Round(timer).ToString();
        if (timer <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void IncreaseScore(float scoreInc)
    {
        score += scoreInc;
        scoreText.text = "Score : " + score.ToString();
    }
}
