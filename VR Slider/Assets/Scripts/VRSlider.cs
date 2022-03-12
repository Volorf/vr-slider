using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class VRSlider : MonoBehaviour
{
    public VRSliderSettings settings;
    
    public bool canBeInteracted = false;
    public BordersManager bordersManager;

    public UnityEvent onSliderIn;
    public UnityEvent onSliderOut;

    public float offset;

    [SerializeField] private TextMesh text;
    
    private Vector3 _snappedHandPosition;
    private Vector3 _snappedButtonDir;
    
    private Vector3 _handDirVec;
    private Transform _handTransform;

    private float _tempOffset = 0;
    private float _counterStep;
    private int _counter = 0;

    private bool _hasBeenReseted = false;
    private float _dur;

    private float _limit;


    private void Start()
    {
        _counterStep = settings.step;
        _dur = settings.dur;
        text.text = "0";

        _limit = settings.step * (settings.stepCountLimit + 1);
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
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 5f * Time.deltaTime);
            transform.DOLocalMoveY(0, _dur);
        }
        
        if (Input.GetKey(KeyCode.Space) && canBeInteracted)
        {
            _hasBeenReseted = false;
            text.text = _counter.ToString();
            
            _handDirVec = Helper.GetHandDirection(_snappedHandPosition, _handTransform.position);
            // Debug.DrawLine(_snappedHandPosition, _handTransform.position, Color.blue);
            
            float angle = Vector3.Angle(_snappedButtonDir, _handDirVec) * Mathf.Deg2Rad;
            offset = _handDirVec.magnitude * Mathf.Cos(angle);
            
            // Debug.DrawLine(_snappedHandPosition, _snappedHandPosition - transform.up * offset, Color.red);
            
            if (offset > _limit || offset < -_limit) return;
            
            if (offset >= _tempOffset + _counterStep)
            {
                _counter--;
                _tempOffset += _counterStep;
                bordersManager.Up();
            }
            
            if (offset <= _tempOffset - _counterStep)
            {
                _counter++;
                _tempOffset -= _counterStep;
                bordersManager.Down();
            }

            print("temp offset: " + _tempOffset);
            print("offset: " + offset);
            
            // Clamp offset 
            // TODO fix hardcoded values
            
            
            transform.localPosition = new Vector3(0, -offset, 0);
            // print("angle is " + angle);
        }
        else
        {
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 5f * Time.deltaTime);
            // transform.DOLocalMoveY(0f, 1f);
            // if (!_hasBeenResetted) return;
            // bordersManager.Reset();
            _tempOffset = 0;
            _hasBeenReseted = true;
        }

        
        // else
        // {
        //     
        // }
    }
}
