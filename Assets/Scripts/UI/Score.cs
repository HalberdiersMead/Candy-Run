using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private Player playerScore;
    [SerializeField] private TMP_Text scoreTxt;

    private void Start()
    {
        score = playerScore.score;
    }
    private void Update()
    {
        scoreTxt.text = "Score: " + score.ToString();
    }
}
