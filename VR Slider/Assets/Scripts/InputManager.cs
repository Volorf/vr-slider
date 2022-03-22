using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent onTriggerDown;
    public UnityEvent onTriggerUp;

    private bool _hasBeenPressedOnce = false;
    private bool _hasBeenPressed = false;

    private bool _hasBeenReleasedOnce = false;
    private bool _hasBeenReleased = false;

    [SerializeField] [Range(0.0f, 1.0f)] private float triggerThreshold = 0.5f;
    
    void Update()
    {
        OVRInput.Update();
        
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) >= triggerThreshold)
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
        
        if (_hasBeenPressed) onTriggerDown.Invoke();
        if (_hasBeenReleased) onTriggerUp.Invoke();
    }
}
