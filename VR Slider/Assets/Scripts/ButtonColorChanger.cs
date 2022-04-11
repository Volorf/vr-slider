using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorChanger : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color normalColor;

    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private TextMesh textMesh;
    void Start()
    {
        SetNormalColor();
    }

    public void SetActiveColor()
    {
        renderer.material.color = activeColor;
        textMesh.color = activeColor;
        // print("set active color");
    } 

    public void SetNormalColor()
    {
        renderer.material.color =  normalColor;
        textMesh.color = normalColor;
        // print("set normal color");
    }
}
