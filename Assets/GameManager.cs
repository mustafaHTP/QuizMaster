using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject quizCanvas;
    [SerializeField] private GameObject winScreenCanvas;

    private QuizController _quizController;

    private void Awake()
    {
        _quizController = quizCanvas.GetComponentInChildren<QuizController>();

        quizCanvas.SetActive(true);
        winScreenCanvas.SetActive(false);
    }

    private void Update()
    {
        bool isGameOver = _quizController.IsGameOver;

        if (isGameOver)
        {
            quizCanvas.SetActive(false);
            winScreenCanvas.SetActive(true);
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
