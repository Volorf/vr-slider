using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Create VR Slider Settings")]
public class VRSliderSettings : ScriptableObject
{
    [Header("Base")]
    public float step;
    public int counter;
    
    [Header("Animation")]
    public float dur;
    public float snapSpeed;
    public int stepCountLimit;
    public float expendDur;
    public float collapseDur;
}
