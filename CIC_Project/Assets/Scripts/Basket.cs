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
    
    private TMP_Text counterFormsText;
    public GameObject canvas;
    public int countWinForms;

    public bool basketSelected;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (basketSelected)
        {
            GameObject target = other.gameObject;
            InteractableForm interactable = target.GetComponentInParent<InteractableForm>();
            if (!interactable) return;
            if (interactable.formType == formType && interactable.formColor == formColor)
            {
                countWinForms++;
                counterFormsText.text = countWinForms.ToString();
                if (countWinForms == GeneratorGame.Instance.randomCount)
                {

                }
            }
            else
            {
                GeneratorGame.Instance.counterAttempts--;
                countWinForms--;
            }
        }
        else
        {
            GeneratorGame.Instance.counterAttempts--;
            countWinForms--;
            counterFormsText.text = countWinForms.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (basketSelected)
        {
            GameObject target = other.gameObject;
            InteractableForm interactable = target.GetComponentInParent<InteractableForm>();
            if (!interactable) return;
            if (interactable.formType == formType && interactable.formColor == formColor)
            {
                countWinForms--;
            }
            else
            {
                countWinForms++;
            }
        }
        else
        {
            
        }

    }
}


