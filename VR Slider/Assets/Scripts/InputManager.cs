using UnityEngine;
using UnityEngine.Events;

// TODO: Add hand tracking
public enum InputMode
{
    VR,
    Desktop,
    Hands
}

// TODO: Control Input mode programmatically — check VR or Desktop + switch according gameobjects

public class InputManager : MonoBehaviour
{
    [SerializeField] private OVRHand rightHand;
    [SerializeField] private OVRHand leftHand;

    private bool _isPrightIndexFingerPinching = false;
    
    [Range(0.0f, 1.0f)] public float triggerThreshold = 0.5f;

    public InputMode currentMode = InputMode.Desktop;
    
    public UnityEvent onTriggerDown;
    public UnityEvent onTriggerUp;
    
    private bool _hasBeenPressedOnce = false;
    private bool _hasBeenPressed = false;

    private bool _hasBeenReleasedOnce = false;
    private bool _hasBeenReleased = false;
    
    void Update()
    {
        if (currentMode == InputMode.Desktop)
        {
            _hasBeenPressed = Input.GetKeyDown(KeyCode.Space);
            _hasBeenReleased = Input.GetKeyUp(KeyCode.Space);
        }

        // if (_hasBeenPressed) print("hasbeenpressed");
        // if (_hasBeenReleased) print("hasbeenreleased");

        if (currentMode == InputMode.VR)
        {
            OVRInput.Update();

            float rightIndexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
            float leftIndexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
            
            if (rightIndexTrigger >= triggerThreshold || leftIndexTrigger >= triggerThreshold)
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
        }

        if (currentMode == InputMode.Hands)
        {
        }

        if (_hasBeenPressed) onTriggerDown.Invoke();
        if (_hasBeenReleased) onTriggerUp.Invoke();
    }
}
