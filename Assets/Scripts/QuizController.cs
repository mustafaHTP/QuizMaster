using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizController : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private List<QuestionSO> questions;

    private QuestionSO _currentQuestion;

    [Header("Buttons")]
    [SerializeField] private Button[] buttons;

    [Header("Question TMP")]
    [SerializeField] private TextMeshProUGUI questionTMP;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultAnswerSprite;
    [SerializeField] private Sprite correctAnswerSprite;

    [Header("Slider")]
    [SerializeField] private Slider progressBar;

    [Header("Timer")]
    [SerializeField] private Timer timer;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreTMP;

    private ScoreManager _scoreManager;

    //States
    private bool _isAnsweringQuestion;
    private bool _hasEarlyAnswered;
    private bool _isGameOver;

    public bool IsGameOver { get => _isGameOver; }

    private void Awake()
    {
        //Set Progress bar
        progressBar.maxValue = questions.Count;

        //Set score
        _scoreManager = scoreTMP.GetComponent<ScoreManager>();
        scoreTMP.text = "Score: 0%";

        _isAnsweringQuestion = false;
        _isGameOver = false;
        _isAnsweringQuestion = true;
        timer.CountdownForQuestion();
    }

    private void Start()
    {
        GetNextQuestion();
    }

    private void Update()
    {
        if (_isAnsweringQuestion)
        {
            if (timer.IsTimeUp)
            {
                _isAnsweringQuestion = false;
                timer.CountdownForAnswerFeedback();

                if (!_hasEarlyAnswered)
                {
                    ShowCorrectAnswer(false);
                    UpdateScore();
                }
                else
                {
                    _hasEarlyAnswered = false;
                }
            }

            if (_hasEarlyAnswered)
            {
                timer.CancelTimer();
            }
        }
        else
        {
            if (timer.IsTimeUp)
            {
                _isAnsweringQuestion = true;
                timer.CountdownForQuestion();
                GetNextQuestion();
            }
        }
    }

    private void DisplayQuestion()
    {
        questionTMP.text = _currentQuestion.Question;

        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI buttonTMP = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonTMP.text = _currentQuestion.Answers[i];
        }
    }

    public void OnAnswerSelected(int selectedButtonIndex)
    {
        _hasEarlyAnswered = true;

        if (selectedButtonIndex == _currentQuestion.CorrectAnswerIndex)
        {
            _scoreManager.IncrementCorrectAnswer();

            ShowCorrectAnswer(true);
        }
        else
        {
            ShowCorrectAnswer(false);
        }

        UpdateScore();
    }

    private void UpdateScore()
    {
        int score = _scoreManager.CalculateScore();
        scoreTMP.text = $"Score: {score}%";
    }

    private void ShowCorrectAnswer(bool isAnswerTrue)
    {
        //Disable all buttons
        SetButtonState(false);

        int correctAnswerIndex = _currentQuestion.CorrectAnswerIndex;

        //Change question text based on answer correctness
        if (isAnswerTrue)
        {
            questionTMP.text = "Correct !";
        }
        else
        {
            questionTMP.text = "Sorry, the correct answer was: \n"
                + _currentQuestion.Answers[correctAnswerIndex];
        }

        //Change button sprite
        Image correctButtonImage = buttons[correctAnswerIndex].GetComponent<Image>();
        correctButtonImage.sprite = correctAnswerSprite;
    }

    private void SetButtonState(bool isInteractable)
    {
        buttons.ToList().ForEach(btn => btn.interactable = isInteractable);
    }

    private void SetDefaultButtonSprite()
    {
        foreach (var button in buttons)
        {
            Image buttonImage = button.GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            _scoreManager.IncrementAnsweredQuestions();
            progressBar.value = _scoreManager.NumberOfAnsweredQuestions;

            GetRandomQuestion();
            SetButtonState(true);
            SetDefaultButtonSprite();
            DisplayQuestion();
        }
        else
        {
            _isGameOver = true;
        }
    }

    private void GetRandomQuestion()
    {
        if (questions.Contains(_currentQuestion))
        {
            questions.Remove(_currentQuestion);
        }

        if (questions.Count == 0) return;

        int nextQuestionIndex = Random.Range(0, questions.Count - 1);

        _currentQuestion = questions[nextQuestionIndex];
    }
}
