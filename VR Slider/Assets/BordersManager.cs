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
    
    public float step = 0.5f;
    private float _targetOffset = 0;
    private float _targetLimit = 1.5f;
    

    public void Up()
    {
        if (_targetOffset >= _targetLimit) return;
        _targetOffset += step;
        transform.DOLocalMoveY(_targetOffset, 1f).SetEase(Ease.Linear);
    }
    
    public void Down()
    {
        if (_targetOffset <= -_targetLimit) return;
        _targetOffset -= step;
        transform.DOLocalMoveY(_targetOffset, 1f);
    }

    public void Expand()
    {
        expandBorders.Invoke();
    }

    public void Collapse()
    {
        collapseBorders.Invoke();
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
            Collapse();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Expand();
        }
        
    }
}
