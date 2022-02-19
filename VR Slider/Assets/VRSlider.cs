using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSlider : MonoBehaviour
{
    public bool canBeInteracted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            
        }
    }
}
