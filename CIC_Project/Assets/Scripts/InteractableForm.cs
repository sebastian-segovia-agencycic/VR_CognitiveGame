using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FormType { Cube, Prism, Sphere }
public enum ColorType { Yellow, Blue, Red }

public class InteractableForm : MonoBehaviour
{
    public FormType formType;
    public ColorType formColor;
    public Color color;
    private MeshRenderer meshRenderer;

    void Start()
    {
        color = SetColor();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.color = color;
    }

    private Color SetColor()
    {
        if (formColor == (int)ColorType.Yellow)
            return Color.yellow;
        else if ((int)formColor == (int)ColorType.Blue)
            return Color.blue;
        else
            return Color.red;
    }

}