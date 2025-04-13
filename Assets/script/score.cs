using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour 
{
    int playerscore = 0;
    int highscore = 0;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;

    void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0); // Load saved high score
        UpdateUI();
    }

    public void updatescore(int enemylevel)
    {
        playerscore += enemylevel;
        UpdateUI();
        CheckHighScore();
    }

    void CheckHighScore()
    {
        if (playerscore > highscore) // If new score is higher, update highscore
        {
            highscore = playerscore;
            PlayerPrefs.SetInt("HighScore", highscore); // Save highscore
            PlayerPrefs.Save();
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + playerscore;
        highScoreText.text = "High Score: " + highscore;
    }
}
