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

    public float offset;

    [SerializeField] private TextMesh text;
    
    private Vector3 _snappedHandPosition;
    private Vector3 _snappedButtonDir;
    
    private Vector3 _handDirVec;
    private Transform _handTransform;

    private float _tempOffset = 0;
    private float _counterStep = 0.5f;
    private int _counter = 0;


    private void Start()
    {
        text.text = "0";
    }
    
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _tempOffset = 0;
            _snappedHandPosition = _handTransform.position;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            canBeInteracted = false;
            _tempOffset = 0;
        }

        if (!canBeInteracted)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 5f * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.Space) && canBeInteracted)
        {
            text.text = _counter.ToString();
            
            _handDirVec = Helper.GetHandDirection(_snappedHandPosition, _handTransform.position);
            // Debug.DrawLine(_snappedHandPosition, _handTransform.position, Color.blue);
            
            float angle = Vector3.Angle(_snappedButtonDir, _handDirVec) * Mathf.Deg2Rad;
            offset = _handDirVec.magnitude * Mathf.Cos(angle);
            // Debug.DrawLine(_snappedHandPosition, _snappedHandPosition - transform.up * offset, Color.red);

            if (offset > _tempOffset + _counterStep)
            {
                _counter--;
                _tempOffset += _counterStep;
            }
            
            if (offset < _tempOffset - _counterStep)
            {
                _counter++;
                _tempOffset -= _counterStep;
            }

            print("temp offset: " + _tempOffset);
            print("offset: " + offset);

            transform.localPosition = new Vector3(0, -offset, 0);
            // print("angle is " + angle);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 5f * Time.deltaTime);
        }

        
        // else
        // {
        //     
        // }
    }
}
