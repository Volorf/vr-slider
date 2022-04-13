using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorChanger : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color activeTextColor;

    private Color _normTextColor;

    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private TextMesh textMesh;
    void Start()
    {
        _normTextColor = textMesh.color;
        SetNormalColor();
    }

    public void SetActiveColor()
    {
        renderer.material.color = activeColor;
        textMesh.color = activeTextColor;
        // print("set active color");
    } 

    public void SetNormalColor()
    {
        renderer.material.color = normalColor;
        textMesh.color = _normTextColor;
        // print("set normal color");
    }
}
