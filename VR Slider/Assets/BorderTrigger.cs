using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BorderTrigger : MonoBehaviour
{
    private float dur = 0.5f;
    public void MoveToTargetY(float targetY)
    {
        transform.DOLocalMoveY(targetY, dur);
    }
}
