using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Counter : MonoBehaviour
{

    private Vector3 _snappedHandPosition;
    private Vector3 _snappedButtonDir;
    private Vector3 _handDirVec;

    private float _triggerOffset = 0.25f;
    private float _buttonOffset = 0.5f;
    private Transform _handTransform;
    
    private bool _isHandTouching = false;
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        _snappedHandPosition = other.transform.position;
        
        if (other.CompareTag("Hand"))
        {
            _handTransform = other.transform;
            print("it's a hand.");
            _isHandTouching = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            _snappedButtonDir = transform.position - transform.up;
            _isHandTouching = false;
            print("the hand is gone.");
            // transform.localPosition = new Vector3(0, 0, 0);
        }
    }


    

    // Update is called once per frame
    void Update()
    {
        if (_isHandTouching)
        {
            _handDirVec = GetHandDirection(_snappedHandPosition, _handTransform.position);
            // print("_handDirVec: " + _handDirVec);
            Debug.DrawLine(_snappedHandPosition, _handTransform.position, Color.blue);
            float angle = Vector3.Angle(_snappedButtonDir, _handDirVec * -1);
            float radFromDegs = Mathf.Deg2Rad * angle;
            float cMag = _handDirVec.magnitude * Mathf.Cos(radFromDegs);
            // print("cMag: " + cMag);
            Debug.DrawLine(_snappedHandPosition - transform.position, _snappedHandPosition - transform.up * cMag, Color.black);
            float cMagClamp = Mathf.Clamp(cMag, _triggerOffset, _buttonOffset + _triggerOffset);
            transform.localPosition = new Vector3(0, -cMagClamp + _triggerOffset, 0);
            // print("angle: " + angle);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 3f * Time.deltaTime);
        }

        // print("button up vec: " + transform.up);
        
        
        Debug.DrawLine(transform.position, transform.up, Color.red);
        
        // print("local pos: " + transform.position);
        // print("distance: " + _distance);
    }

    public void SnapshotPosition()
    {
        // _snapPosition = transform.position;
    }
    
    private Vector3 GetHandDirection(Vector3 snapVec, Vector3 curVec)
    {
        return snapVec - curVec;
    }
}
