using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Basket : MonoBehaviour
{
    //InteratableInfo
    private InteractableForm currentWinInteractable;    
    public ColorType basketColor;
    private float countWinForms;
    private TMP_Text counterPoints;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateWinConditions()
    {
        currentWinInteractable = GeneratorGame.Instance.winInteractableForm;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        InteractableForm interactable = target.GetComponent<InteractableForm>();

        if (interactable.formType == currentWinInteractable.formType && interactable.colorType == currentWinInteractable.colorType)
        {
            countWinForms++;
            if (countWinForms == GeneratorGame.Instance.countRamdon)
            {

            }
        }
        else
        {
            countWinForms--;
        }
    }

    private void CalculateWin()
    {

    }
}


