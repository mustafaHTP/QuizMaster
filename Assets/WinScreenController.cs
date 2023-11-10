using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreenController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameObject finalScore;

    private TextMeshProUGUI finalScoreTMP;

    private void Awake()
    {
        finalScoreTMP = finalScore.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        DisplayEndGameScore();
    }

    private void DisplayEndGameScore()
    {
        int endGameScore = scoreManager.CalculateScore();
        finalScoreTMP.text = $"Congratulations! \nYou Scored {endGameScore}%";
    }
}
