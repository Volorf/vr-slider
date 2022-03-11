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
    public UnityEvent expandBorders;
    public UnityEvent collapseBorders;
    public UnityEvent resetBorders;
    public BorderModerator borderModerator;

    private BordersState _currentState = BordersState.Collapsed;
    
    public float step = 0.5f;
    private float _targetOffset = 0;
    private float _targetLimit = 1.5f;
    private float _dur = 0.2f;

    [SerializeField] private int _offsetCounter = 0;
    

    public void Up()
    {
        _offsetCounter--;
        if (_offsetCounter < -3)
        {
            _offsetCounter = -3;
            return;
        }
        print("_offsetCounter is " + _offsetCounter);
        if (_targetOffset >= _targetLimit) return;
        _targetOffset += step;
        transform.DOLocalMoveY(_targetOffset, _dur).SetEase(Ease.Linear);
    }
    
    public void Down()
    {
        _offsetCounter++;
        if (_offsetCounter > 3)
        {
            _offsetCounter = 3;
            return;
        }
        if (_targetOffset <= -_targetLimit) return;
        _targetOffset -= step;
        transform.DOLocalMoveY(_targetOffset, _dur);
    }

    public void Reset()
    {
        _offsetCounter = 0;
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
        
        int m;

        switch (_offsetCounter)
        {
            case 3: m = 0;
                break;
            case 2: m = 1;
                break;
            case 1: m = 2;
                break;
            case 0: m = 3;
                break;
            case -1: m = 4;
                break;
            case -2: m = 5;
                break;
            case -3: m = 6;
                break;
            default: m = -1;
                break;
        }
        // collapseBorders.Invoke();
        Reset();
        resetBorders.Invoke();
        
        // 3 + x = 0
        // 2 + x = 1
        // 1 + x = 2
        // 0 + x = 3
        // -1 + x = 4
        
        
        borderModerator.SetTargetY(0f, step * m);
    }

    
    // Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Up();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Down();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // Collapse();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Expand();
        }
        
    }
}
