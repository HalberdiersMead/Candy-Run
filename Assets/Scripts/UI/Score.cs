using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    private int score;
    public TMP_Text hstxt;


    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");

        }
        else
        {
            score = 0;
        }
        hstxt.text = "Highscore: " + score.ToString();
    }
}

