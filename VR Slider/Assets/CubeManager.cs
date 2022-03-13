using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private bool _toggle = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.gameObject.SetActive(_toggle);
        OVRInput.Update();
        // if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        // if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
        if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) >= 0.7f)
        {
            _toggle = !_toggle;
        }
        
        
    }
}
