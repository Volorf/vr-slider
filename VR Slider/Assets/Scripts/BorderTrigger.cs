using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BorderTrigger : MonoBehaviour
{
    public VRSliderSettings settings;
    public void MoveToTargetY(float targetY)
    {
        transform.DOLocalMoveY(targetY, settings.collapseDur);
    }
}
