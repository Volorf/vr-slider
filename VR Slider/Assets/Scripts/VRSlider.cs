using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRSlider : MonoBehaviour
{
    public bool canBeInteracted = false;

    public UnityEvent onSliderIn;
    public UnityEvent onSliderOut;
    
    private Vector3 _snappedHandPosition;
    private Vector3 _snappedButtonDir;
    
    private Vector3 _handDirVec;
    private Transform _handTransform;
    
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            onSliderIn.Invoke();
            canBeInteracted = true;
            
            
            _handTransform = other.transform;

            Transform buttonTransform = transform;
            _snappedButtonDir = buttonTransform.up;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            onSliderOut.Invoke();
            canBeInteracted = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && canBeInteracted)
        {
            _handDirVec = Helper.GetHandDirection(_snappedHandPosition, _handTransform.position);
            // Debug.DrawLine(_snappedHandPosition, _handTransform.position, Color.blue);
            
            float angle = Vector3.Angle(_snappedButtonDir, _handDirVec) * Mathf.Deg2Rad;
            float offset = _handDirVec.magnitude * Mathf.Cos(angle);
            // Debug.DrawLine(_snappedHandPosition, _snappedHandPosition - transform.up * offset, Color.red);

            transform.localPosition = new Vector3(0, -offset, 0);
            // print("angle is " + angle);
            print("yo");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _snappedHandPosition = _handTransform.position;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            canBeInteracted = false;
        }

        if (!canBeInteracted)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 3f * Time.deltaTime);
        }
        // else
        // {
        //     
        // }
    }
}
