using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

internal enum BordersState
{
    Collapsed,
    Expanded
}

public class BordersManager : MonoBehaviour
{
    public VRSliderSettings settings;
    
    public UnityEvent expandBorders;
    public UnityEvent collapseBorders;
    public UnityEvent resetBorders;
    public BorderModerator borderModerator;

    private BordersState _currentState = BordersState.Collapsed;
    
    private float _step;
    private float _targetOffset = 0;
    private float _targetLimit;
    private float _snapSpeed;

    [SerializeField] private int offsetCounter = 0;

    private void Start()
    {
        _step = settings.step;
        _targetLimit = settings.stepCountLimit * _step;
        _snapSpeed = settings.snapSpeed;
    }

    public void Up()
    {
        offsetCounter--;
        if (offsetCounter < -settings.stepCountLimit)
        {
            offsetCounter = -settings.stepCountLimit;
            return;
        }

        if (_targetOffset >= _targetLimit) return;
        _targetOffset += _step;
        transform.DOLocalMoveY(_targetOffset, _snapSpeed).SetEase(Ease.Linear);
    }
    
    public void Down()
    {
        offsetCounter++;
        if (offsetCounter > settings.stepCountLimit)
        {
            offsetCounter = settings.stepCountLimit;
            return;
        }
        if (_targetOffset <= -_targetLimit) return;
        _targetOffset -= _step;
        transform.DOLocalMoveY(_targetOffset, _snapSpeed);
    }

    public void Reset()
    {
        offsetCounter = 0;
        _targetOffset = 0;
        transform.localPosition = Vector3.zero;
        
    }

    public void Expand()
    {
        if (_currentState == BordersState.Expanded) return;
        _currentState = BordersState.Expanded;
        expandBorders.Invoke();
    }

    public void Collapse()
    {
        if(_currentState == BordersState.Collapsed) return;
        _currentState = BordersState.Collapsed;

        int m = offsetCounter switch
        {
            3 => 0,
            2 => 1,
            1 => 2,
            0 => 3,
            -1 => 4,
            -2 => 5,
            -3 => 6,
            _ => -1
        };

        Reset();
        resetBorders.Invoke();
        borderModerator.SetTargetY(0f, _step * m);
    }
}
