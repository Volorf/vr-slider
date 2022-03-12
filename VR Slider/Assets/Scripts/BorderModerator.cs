using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriggerBorder : UnityEvent<float> {};

public class Border
{
    private GameObject _go;
    private Vector3 _initialPosition;

    public Border(GameObject go, Vector3 pos)
    {
        _go = go;
        _initialPosition = pos;
    }

    public void Hide()
    {
        _go.SetActive(false);
    }

    public void Show()
    {
        _go.SetActive(true);
    }
    
    public void ResetPosition()
    {
        _go.transform.localPosition = _initialPosition;
    }

    public void UpdateYPos(float y)
    {
        _go.transform.localPosition = new Vector3(
            _go.transform.localPosition.x,
            _go.transform.localPosition.y + y,
            _go.transform.localPosition.z);
    }
    
}
public class BorderModerator : MonoBehaviour
{
    public VRSliderSettings settings;
    
    private List<Border> _borders = new List<Border>();

    private void Start()
    {
        int index = 0;
        foreach (Transform border in transform)
        {
            Vector3 pos = new Vector3(0f, -index * settings.step);
            index++;
            
            Border b = new Border(border.gameObject, pos);
            _borders.Add(b);
        }
        HideBorders();
    }

    public TriggerBorder triggerBorder;

    public void SetTargetY(float targetY, float yOffset)
    {
        UpdateY(yOffset);
        ShowBorders();
        triggerBorder.Invoke(targetY);
        Invoke("HideBorders", settings.collapseDur);
    }

    private void ShowBorders()
    {
        foreach (Border b in _borders)
        {
            b.Show();
        }
    }

    private void UpdateY(float y)
    {
        foreach (Border b in _borders)
        {
            b.ResetPosition();
            b.UpdateYPos(y);
        }
    }

    private void HideBorders()
    {
        foreach (Border b in _borders)
        {
            b.Hide();
        }
    }
}
