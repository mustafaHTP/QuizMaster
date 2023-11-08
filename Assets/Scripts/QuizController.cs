using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [SerializeField] private QuestionSO question;
    [SerializeField] private Button[] buttons;
    [SerializeField] private TextMeshProUGUI questionTMP;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultAnswerSprite;
    [SerializeField] private Sprite correctAnswerSprite;

    [SerializeField] private Timer timer;

    private void Start()
    {
        GetNextQuestion();
    }

    private void Update()
    {
        
    }

    private void DisplayQuestion()
    {
        questionTMP.text = question.Question;

        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI buttonTMP = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonTMP.text = question.Answers[i];
        }
    }

    public void OnAnswerSelected(int selectedButtonIndex)
    {
        Image correctButtonImage;
        int correctAnswerIndex = question.CorrectAnswerIndex;

        if (selectedButtonIndex == question.CorrectAnswerIndex)
        {
            questionTMP.text = "Correct !";

            //Change button sprite
            correctButtonImage = buttons[correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
        }
        else
        {
            //Change question text to correct answer
            questionTMP.text = "Sorry, the correct answer was: \n"
                + question.Answers[correctAnswerIndex];

            //Change button sprite
            correctButtonImage = buttons[correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
        }

        //Disable all buttons after user select answer
        SetButtonState(false);
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
        SetButtonState(true);
        SetDefaultButtonSprite();
        DisplayQuestion();
    }
}
