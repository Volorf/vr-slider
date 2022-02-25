using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSlider_old : MonoBehaviour
{
    public bool canBeInteracted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            canBeInteracted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            canBeInteracted = false;
        } 
    }
}
