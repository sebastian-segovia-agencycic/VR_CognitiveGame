using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Basket : MonoBehaviour
{
    //InteratableInfo
    public ColorType formColor;
    public FormType formType;

    public ColorType basketColor;
    InteractableForm interactable;
    public TMP_Text counterFormsText;
    public GameObject canvas;
    public int countWinForms, currentCountWin;

    public bool basketSelected;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        interactable = target.GetComponentInParent<InteractableForm>();

        if (!interactable) return;

        if (basketSelected)
        {
            if (interactable.formType == formType && interactable.formColor == formColor)
            {
                currentCountWin++;
                counterFormsText.text = currentCountWin.ToString();
                if (currentCountWin == GeneratorGame.Instance.randomCount)
                {

                }
            }
            else
            {
                GeneratorGame.Instance.counterAttempts--;
                currentCountWin--;
            }
        }
        else
        {
            GeneratorGame.Instance.counterAttempts--;
            currentCountWin--;
            counterFormsText.text = currentCountWin.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (basketSelected)
        {
            GameObject target = other.gameObject;
            interactable = target.GetComponentInParent<InteractableForm>();
            if (!interactable) return;
            if (interactable.formType == formType && interactable.formColor == formColor)
            {
                currentCountWin--;
            }
            else
            {
                currentCountWin++;
            }
        }
        else
        {
            
        }

    }
}


