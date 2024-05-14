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
    public TMP_Text counterFormsText;
    public GameObject canvas;
    public bool basketSelected, activated;

    private void OnTriggerEnter(Collider other)
    {
        if (!activated) return;

        GameObject target = other.gameObject;
        InteractableForm interactable = target.GetComponentInParent<InteractableForm>();

        if (!interactable) return;

        if (basketSelected)
        {
            if (interactable.formType == formType && interactable.formColor == formColor)
                GeneratorGame.Instance.currentCountWin++;
            else
            {
                GeneratorGame.Instance.counterAttempts--;
                GeneratorGame.Instance.currentCountWin--;
            }
        }
        else
        {
            GeneratorGame.Instance.counterAttempts--;
            GeneratorGame.Instance.currentCountWin--;
            
        }
        GeneratorGame.Instance.counterAtempsText.text = GeneratorGame.Instance.counterAttempts.ToString();
        counterFormsText.text = GeneratorGame.Instance.currentCountWin.ToString();
        StartCoroutine(ReturnInteractableToPool(target));
    }

    private IEnumerator ReturnInteractableToPool(GameObject obj)
    {
        yield return new WaitForSeconds(3f);
        Spawner.ReturnObjectToPool(obj);
    }
}


