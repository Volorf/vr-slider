using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Create VR Slider Settings")]
public class VRSliderSettings : ScriptableObject
{
    public float step;
    public float dur;
    public float snapSpeed;
    public int stepCountLimit;
    public float expendDur;
    public float collapseDur;
    public int counter;
}
