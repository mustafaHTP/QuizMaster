using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Time Settings")]
    [Space(5)]
    [SerializeField] private float timeToCompleteQuestion;
    [SerializeField] private float timeToShowCorrectAnswer;

    [SerializeField] private Image timerImage;

    public bool IsTimeUp;

    private float _currentTime;
    private float _currentMaxAmount;

    public void CancelTimer()
    {
        _currentTime = 0f;
    }

    public void SetMaxTime(bool isAnsweringQuestion)
    {
        if (isAnsweringQuestion)
        {
            _currentMaxAmount = timeToCompleteQuestion;
        }
        else
        {
            _currentMaxAmount += timeToShowCorrectAnswer;
        }
    }

    private void Update()
    {
        Countdown();
        UpdateTimerImage();
    }

    private void UpdateTimerImage()
    {
        float normalizedTime = _currentTime / _currentMaxAmount;
        timerImage.fillAmount = normalizedTime;
    }

    private void Countdown()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime < 0f)
        {
            IsTimeUp = true;
        }
    }
}