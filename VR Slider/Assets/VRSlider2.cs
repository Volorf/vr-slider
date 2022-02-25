using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRSlider2 : MonoBehaviour
{
    public bool canBeInteracted = false;
    
    public UnityEvent onSliderIn;
    public UnityEvent onSliderOut;
    private void OnTriggerEnter(Collider other)
    {
        onSliderIn.Invoke();
        canBeInteracted = true;
    }

    private void OnTriggerExit(Collider other)
    {
        onSliderOut.Invoke();
        canBeInteracted = false;
    }
}
