using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource _audio;

    private bool _isRTouchVibrating = false;
    
    // private static FXManager _instance;
    // public static FXManager Instance => _instance;

    private void Awake()
    {
        // if (_instance != null && _instance != this)
        // {
        //     Destroy(this.gameObject);
        // }
        // else
        // {
        //     _instance = this;
        // }
        _audio = GetComponent<AudioSource>();
    }
    
    

    public void VibrateLeftHand() => Vibrate(OVRInput.Controller.LTouch); 
    public void VibrateRightHand() => Vibrate(OVRInput.Controller.RTouch);


    private void Vibrate(OVRInput.Controller controller)
    {
        OVRHapticsClip hapticClip = new OVRHapticsClip();

        for (int i = 0; i < 100; i++)
        {
            hapticClip.WriteSample(i % 3 == 0 ? (byte) 0 : (byte) 100);
        }
        
        if (controller == OVRInput.Controller.LTouch)
        {
            OVRHaptics.LeftChannel.Preempt(hapticClip);
            
        }
        if (controller == OVRInput.Controller.RTouch)
        {
            // OVRHaptics.RightChannel.Preempt(hapticClip);
            // OVRHaptics.Channels[0].Mix(hapticClip);
            // OVRHaptics.Channels[1].Mix(hapticClip);
            if(_isRTouchVibrating) return;
            StartCoroutine(nameof(PlayVibroR));
        }
        // OVRInput.SetControllerVibration(1.0f, 1.0f, controller);
    }

    private IEnumerator PlayVibroR()
    {
        _audio.PlayOneShot(audioClip);
        _isRTouchVibrating = true;
        float time = 0.2f;
        float timeCounter = 0f;
        int counter = 0;

        while (timeCounter < time)
        {
            if (counter % 3 == 0)
            {
                OVRInput.SetControllerVibration(1.0f, 1.0f, OVRInput.Controller.RTouch);
            }
            else
            {
                OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
            }

            counter++;
            timeCounter += Time.deltaTime;
            yield return null;
        }

        _isRTouchVibrating = false;
    }
}
