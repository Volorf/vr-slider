using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private bool _toggle = true;
    private MeshRenderer mr;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        mr.enabled = _toggle;
        
        // if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        // if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
        
        // OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.RTouch)
        if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) >= 0.5f)
        {
            _toggle = false;
        }
        else
        {
            _toggle = true;
        }
        
        
    }
}
