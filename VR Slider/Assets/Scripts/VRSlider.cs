using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

[Serializable] public class HandEvent : UnityEvent<VRHand> {}

// TODO: fix a bug with interacting animation via collision exit

public class VRSlider : MonoBehaviour
{
    public VRSliderSettings settings;
    
    public BordersManager bordersManager;

    public UnityEvent onSliderIn;
    public UnityEvent onSliderOut;

    public HandEvent onCounterIncreased;
    public HandEvent onCounterDescreased;

    public float offset;

    [SerializeField] private TextMesh text;
    // [SerializeField] private TextMeshProUGUI text;
    
    private Vector3 _snappedHandPosition;
    private Vector3 _snappedButtonDir;
    
    private Vector3 _handDirVec;
    private Transform _handTransform;

    private float _tempOffset = 0;
    private float _counterStep;
    private int _counter;
    private float _dur;
    
    private bool _canBeInteracted = false;
    private float _limit;

    private bool _isPressing = false;

    private bool _isHoldIncreasingRunning = false;

    private VRHand _interactingHand;

    private Vector3 _refPos;
    private float _camZSmoothTime = 0.3f;
    
    private void Start()
    {
        _counter = settings.counter;
        _counterStep = settings.step;
        _dur = settings.dur;
        text.text = _counter.ToString();

        _limit = settings.step * (settings.stepCountLimit + 1);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            onSliderIn.Invoke();
            _canBeInteracted = true;

            if (other.gameObject.TryGetComponent(out WhichHand whichHand))
            {
                _interactingHand = whichHand.hand;
            }
            
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
            _tempOffset = 0;
            _canBeInteracted = false;
            _isHoldIncreasingRunning = false;
            StopCoroutine(nameof(AccelerateCounter));
        }
    }
    
    public void Snap()
    {
        _isPressing = true;
        _tempOffset = 0;
        _snappedHandPosition = _handTransform.position; 
    }

    public void Reset()
    {
        _isPressing = false;
        _isHoldIncreasingRunning = false;
        StopCoroutine(nameof(AccelerateCounter));
        
        if (Mathf.Abs(_counter) > 1)
        { 
            _canBeInteracted = false;
            onSliderOut.Invoke();
        }
    }

    private void Update()
    {
        if (!_canBeInteracted)
        {
            transform.DOLocalMoveY(0, _dur);
        }

        if(_isPressing && _canBeInteracted)
        {
            text.text = _counter.ToString();

            _handDirVec = Helper.GetHandDirection(_snappedHandPosition, _handTransform.position);
            
            // Debug.DrawLine(_snappedHandPosition, _handTransform.position, Color.blue);
            
            float angle = Vector3.Angle(_snappedButtonDir, _handDirVec) * Mathf.Deg2Rad;
            offset = _handDirVec.magnitude * Mathf.Cos(angle);
            
            // Debug.DrawLine(_snappedHandPosition, _snappedHandPosition - transform.up * offset, Color.red);

            if (offset > _limit)
            {
                if (!_isHoldIncreasingRunning)
                {
                    StartCoroutine(nameof(AccelerateCounter), -1);
                    _isHoldIncreasingRunning = true;
                }
                return;
            }

            if (offset < -_limit)
            {
                if (!_isHoldIncreasingRunning)
                {
                    StartCoroutine(nameof(AccelerateCounter), 1);
                    _isHoldIncreasingRunning = true;
                }
                return;
            }


            if (offset >= _tempOffset + _counterStep)
            {
                _counter--;
                _tempOffset += _counterStep;
                bordersManager.Up();
                onCounterDescreased.Invoke(_interactingHand);
                StopCoroutine(nameof(AccelerateCounter));
            }
            
            if (offset <= _tempOffset - _counterStep)
            {
                _counter++;
                _tempOffset -= _counterStep;
                bordersManager.Down();
                onCounterIncreased.Invoke(_interactingHand);
                StopCoroutine(nameof(AccelerateCounter));
            }

            // transform.localPosition = new Vector3(0, -offset, 0);
            Vector3 posTarget = new Vector3(0, -offset, 0);
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, posTarget, ref _refPos, _camZSmoothTime);
        }
        else
        {
            _tempOffset = 0;
        }
    }

    private IEnumerator AccelerateCounter(int n)
    {
        float timeLimit = 0.5f;
        float time = 0f;
        int timeCounter = 0;
        
        while (true)
        {
            if (time >= timeLimit)
            {
                time = 0f;
                _counter += n;
                timeCounter++;
            }

            switch (timeCounter)
            {
                case 3:
                    timeLimit = 0.1f;
                    break;
                case 16:
                    timeLimit = 0.05f;
                    break;
            }

            time += Time.deltaTime;
            
            yield return null;
        }
    }
}
