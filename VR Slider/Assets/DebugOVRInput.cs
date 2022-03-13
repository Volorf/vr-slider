using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugOVRInput : MonoBehaviour
{
    public TextMeshPro debugText;

    private string _text = "Debug Text";

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        
        
    }

    void Update()
    {
        
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.RTouch)) return;
        
        OVRInput.Update();
        
        _text = "";
        _text += "Get "     + OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.RTouch) + "\n";
        _text += "GetDown Button " + OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch) + "\n";
        _text += "GetUp "   + OVRInput.GetUp(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.RTouch) + "\n";
        _text += "1D "   + OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) + "\n";
        
        debugText.text = _text;
    }
}
