using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BordersManager : MonoBehaviour
{
    public float step = 0.5f;
    private float _targetOffset = 0;
    private float _targetLimit = 1.5f;
    
    [ContextMenu("Call Up()")]
    public void Up()
    {
        if (_targetOffset >= _targetLimit) return;
        _targetOffset += step;
        transform.DOLocalMoveY(_targetOffset, 1f).SetEase(Ease.Linear);
    }
    
    [ContextMenu("Call Down()")]
    public void Down()
    {
        if (_targetOffset <= -_targetLimit) return;
        _targetOffset -= step;
        transform.DOLocalMoveY(_targetOffset, 1f);
    }

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
    }
}
