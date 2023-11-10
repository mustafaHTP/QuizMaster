using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Time Settings")]
    [Space(5)]
    [SerializeField] private float timeToCompleteQuestion;
    [SerializeField] private float timeToShowAnswerFeedback;

    [SerializeField] private Image timerImage;

    public bool IsTimeUp;

    private float _currentTime;
    private float _countdownTime;

    public void CancelTimer()
    {
        _currentTime = 0f;
    }

    public void CountdownForQuestion()
    {
        IsTimeUp = false;

        _countdownTime = timeToCompleteQuestion;
        _currentTime = timeToCompleteQuestion;
    }

    public void CountdownForAnswerFeedback()
    {
        IsTimeUp = false;

        _countdownTime = timeToShowAnswerFeedback;
        _currentTime = timeToShowAnswerFeedback;
    }

    private void Update()
    {
        Countdown();
        UpdateTimerImage();
    }

    private void UpdateTimerImage()
    {
        float normalizedTime = _currentTime / _countdownTime;
        timerImage.fillAmount = normalizedTime;
    }

    private void Countdown()
    {
        if (_currentTime > 0f)
        {
            _currentTime -= Time.deltaTime;
        }
        else
        {
            IsTimeUp = true;
        }
    }
}