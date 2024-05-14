using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FormType { Cube, Prism, Sphere }
public enum ColorType { Yellow, Blue, Red }

[RequireComponent(typeof(AudioSource))]
public class InteractableForm : MonoBehaviour
{
    public FormType formType;
    public ColorType formColor;
    public ScriptableBaseInteractable baseInteractable; 
    private MeshRenderer meshRenderer;
    private AudioSource audioSource;
    private Color color;
    public bool canUse = true;

    void Start()
    {
        color = SetColor();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.color = color; 
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.6f;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 16;
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

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(baseInteractable.clips[Random.Range(0, baseInteractable.clips.Count)]);
    }
}
