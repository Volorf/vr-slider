using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class VRSlider : MonoBehaviour
{
    public VRSliderSettings settings;
    
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
    private float _dur;
    
    private bool _canBeInteracted = false;
    private float _limit;

    private bool _isPressing = false;
    
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
            _canBeInteracted = true;
            
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
            Debug.DrawLine(_snappedHandPosition, _handTransform.position, Color.blue);
            
            float angle = Vector3.Angle(_snappedButtonDir, _handDirVec) * Mathf.Deg2Rad;
            offset = _handDirVec.magnitude * Mathf.Cos(angle);
            
            Debug.DrawLine(_snappedHandPosition, _snappedHandPosition - transform.up * offset, Color.red);
            
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
            
            transform.localPosition = new Vector3(0, -offset, 0);
        }
        else
        {
            _tempOffset = 0;
        }
    }
}
