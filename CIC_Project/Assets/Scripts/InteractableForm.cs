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
    public bool canUse = true;

    void Start()
    {
        color = SetColor();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.color = color;
    }

    private Color SetColor()
    {
        if (formColor == ColorType.Yellow)
            return Color.yellow;
        else if (formColor == ColorType.Blue)
            return Color.blue;
        else
            return Color.red;
    }

}
