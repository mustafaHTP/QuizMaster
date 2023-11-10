using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Quiz Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(1, 4)]
    [SerializeField] private string question = "Enter a new question";
    [SerializeField] private string[] answers;
    [Range(0, 3)]
    [SerializeField] private int correctAnswerIndex;

    public string Question { get => question; }
    public string[] Answers { get => answers; }
    public int CorrectAnswerIndex { get => correctAnswerIndex; }

    public string GetCorrectAnswer()
    {
        if (correctAnswerIndex >= 0 && correctAnswerIndex < answers.Length - 1)
        {
            return answers[correctAnswerIndex];
        }
        else
        {
            throw new IndexOutOfRangeException();
        }
    }
}
