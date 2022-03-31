using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibroSetup
{
    public OVRInput.Controller controller;
    public bool isVibrating = false;
    public VibroSetup(OVRInput.Controller c)
    {
        controller = c;
    }
}
public class FXManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource _audio;

    private bool _isRTouchVibrating = false;

    private VibroSetup _rightVibroSetup = null;
    private VibroSetup _leftVibroSetup = null;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (OVRInput.Controller.RTouch != null)
        {
            _rightVibroSetup = new VibroSetup(OVRInput.Controller.RTouch);
        }

        if (OVRInput.Controller.LTouch != null)
        {
            _leftVibroSetup = new VibroSetup(OVRInput.Controller.LTouch);
        }
    }


    // public void VibrateLeftHand() => Vibrate(OVRInput.Controller.LTouch); 
    // public void VibrateRightHand() => Vibrate(OVRInput.Controller.RTouch);


    public void Vibrate(VRHand vrHand)
    {
        // OVRHapticsClip hapticClip = new OVRHapticsClip();
        //
        // for (int i = 0; i < 100; i++)
        // {
        //     hapticClip.WriteSample(i % 3 == 0 ? (byte) 0 : (byte) 200);
        // }
        
        if (vrHand == VRHand.Left)
        {
            _audio.PlayOneShot(audioClip);
            print("leftVibroSetup.controller: " + _leftVibroSetup.controller);
            if(_leftVibroSetup == null) return;
            if(_leftVibroSetup.isVibrating) return;
            if (_leftVibroSetup.controller != null)
            {
                StartCoroutine(nameof(PlayVibro), _leftVibroSetup);
            }
        }
        
        if (vrHand == VRHand.Right)
        {
            _audio.PlayOneShot(audioClip);
            if(_rightVibroSetup == null) return;
            if(_rightVibroSetup.isVibrating) return;
            if (_rightVibroSetup.controller != null)
            {
                StartCoroutine(nameof(PlayVibro), _rightVibroSetup);
            }
        }
    }

    private IEnumerator PlayVibro(VibroSetup vibroSetup)
    {
        vibroSetup.isVibrating = true;
        float time = 0.2f;
        float timeCounter = 0f;
        int counter = 0;
        
        
        while (timeCounter < time)
        {
            if (counter % 3 == 0)
            {
                OVRInput.SetControllerVibration(1.0f, 1.0f, vibroSetup.controller);
            }
            else
            {
                OVRInput.SetControllerVibration(0f, 0f, vibroSetup.controller);
            }

            counter++;
            timeCounter += Time.deltaTime;
            yield return null;
        }

        vibroSetup.isVibrating = false;
    }
}
