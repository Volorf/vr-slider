using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BorderMover : MonoBehaviour
{

    // public UnityEvent chainCollapse;
    public UnityEvent chainExpand;
    // public UnityEvent chainReset;
    
    public float offset = 0.5f;
    private float _dur = 0.1f;

    public void SetDuration(float dur)
    {
        _dur = dur;
    }
    public void Collapse()
    {
        // transform.DOLocalMoveY(0f, _dur).OnComplete(CallChainCollapse);
    }

    public void Expand()
    {
        transform.DOLocalMoveY(offset, _dur).OnComplete(CallChainExpand);
    }

    public void Reset()
    {
        transform.localPosition = Vector3.zero;
        // chainReset.Invoke();
    }

    private void CallChainCollapse()
    {
        // chainCollapse.Invoke();
    }

    private void CallChainExpand()
    {
        chainExpand.Invoke();
    }
}
