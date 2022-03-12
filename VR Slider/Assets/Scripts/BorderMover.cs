using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
    Up,
    Down
}

public class BorderMover : MonoBehaviour
{
    public Direction direction = Direction.Up;
    public VRSliderSettings settings;
    public UnityEvent chainExpand;

    private float _offset;
    private float _dur;

    private void Start()
    {
        _offset = settings.step * (direction == Direction.Up ? 1f : -1f);
        _dur = settings.expendDur;
    }
    public void Collapse()
    {
        // transform.DOLocalMoveY(0f, _dur).OnComplete(CallChainCollapse);
    }

    public void Expand()
    {
        transform.DOLocalMoveY(_offset, _dur).OnComplete(CallChainExpand);
    }

    public void Reset()
    {
        transform.localPosition = Vector3.zero;
    }

    private void CallChainExpand()
    {
        chainExpand.Invoke();
    }
}
