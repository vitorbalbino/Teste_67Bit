using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillArea : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] PlayerDistanceChecker m_DistanceChecker;
    [SerializeField] float _waitTime = 2.0f;
    [SerializeField] float _currentTimer = 0;


    [SerializeField] bool _isFilled = false;
    [SerializeField] bool _eventCalled = false;

    public event Action _Filled;



    public bool IsFilled()
    {
        return _currentTimer > _waitTime;
    }



    float FillRatio()
    {
        if (_waitTime == 0)
        {
            Debug.LogError("Error: _waitTime was 0 in division!", this);
            return 0f;
        }

        if (_currentTimer < 0)
        {
            return 0f;
        }

        if (_currentTimer > _waitTime)
        {
            return 1f;
        }

        return _currentTimer / _waitTime;
    }



    public void SetDelayNoFill(float delay)
    {
        _currentTimer = -delay;
    }



    private void CheckPlayerDistance()
    {
        if (!m_DistanceChecker.PlayerIsClose())
        {
            _currentTimer = 0;
            _eventCalled = false;
            return;
        }

        if (!IsFilled())
        {
            _currentTimer += Time.fixedDeltaTime;
            _eventCalled = false;
            return;
        }

        if (!_eventCalled)
        {
            _Filled?.Invoke();
            _eventCalled = true;
        }
    }



    private void FixedUpdate()
    {
        CheckPlayerDistance();
    }



    private void Update()
    {
        _fillImage.fillAmount = FillRatio();
    }
}
