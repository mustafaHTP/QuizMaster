using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _numberOfCorrectAnswer;
    private int _numberOfAnsweredQuestions;

    public int NumberOfAnsweredQuestions { get => _numberOfAnsweredQuestions; }

    public void IncrementCorrectAnswer()
    {
        ++_numberOfCorrectAnswer;
    }

    public void IncrementAnsweredQuestions()
    {
        ++_numberOfAnsweredQuestions;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(_numberOfCorrectAnswer / (float)_numberOfAnsweredQuestions * 100);
    }
}
