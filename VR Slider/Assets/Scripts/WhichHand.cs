using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VRHand
{
    Left,
    Right
}

public class WhichHand : MonoBehaviour
{
    public VRHand hand = VRHand.Right;
}
