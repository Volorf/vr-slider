using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugOVRInput : MonoBehaviour
{
    public TextMeshPro debugText;

    private string _text = "Debug Text";
    private float _limit = 0.5f;

    
    private bool _hasBeenPressedOnce = false;
    private bool _hasBeenPressed = false;

    private bool _hasBeenReleasedOnce = false;
    private bool _hasBeenReleased = false;

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        
        
    }

    void Update()
    {
        
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.RTouch)) return;
        
        OVRInput.Update();

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) >= _limit)
        {
            if (!_hasBeenPressedOnce)
            {
                _hasBeenPressed = true;
                _hasBeenPressedOnce = true;
            }
            else
            {
                _hasBeenPressed = false;
            }

            _hasBeenReleasedOnce = false;
        }
        else
        {
            if (!_hasBeenReleasedOnce)
            {
                _hasBeenReleased = true;
                _hasBeenReleasedOnce = true;
            }
            else
            {
                _hasBeenReleased = false;
            }

            _hasBeenPressedOnce = false;
        }
        _text = "";
        _text += "Has Been Pressed: " + _hasBeenPressed.ToString() + "\n";
        _text += "Has Been Pressed Once: " + _hasBeenPressedOnce.ToString() + "\n";
        _text += "Has Been Pressed: " + _hasBeenReleased.ToString() + "\n";
        _text += "Has Been Pressed Once: " + _hasBeenReleasedOnce.ToString() + "\n";
        
        // _text += "Get "     + OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.RTouch) + "\n";
        // _text += "GetDown Button " + OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch) + "\n";
        // _text += "GetUp "   + OVRInput.GetUp(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.RTouch) + "\n";
        // _text += "1D "   + OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) + "\n";
        
        debugText.text = _text;
    }
}
