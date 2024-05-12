using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FormType { Cube, Prism, Sphere }
public enum ColorType { Yellow, Blue, Red }

public class InteractableForm : MonoBehaviour
{
    public FormType formType;
    public ColorType colorType;
    public Color color;
    public MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer.material.color = color;
    }

    void Update()
    {
                
    }
}
