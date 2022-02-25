using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorChanger : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color normalColor;

    private MeshRenderer _renderer;
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        SetNormalColor();
    }

    public void SetActiveColor()
    {
        _renderer.material.color = activeColor;
        print("set active color");
    } 

    public void SetNormalColor()
    {
        _renderer.material.color =  normalColor;
        print("set normal color");
    }
}
