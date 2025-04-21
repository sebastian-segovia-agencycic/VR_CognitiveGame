using System.Collections;
using TMPro;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public ColorType formColor;
    public FormType formType;
    public ColorType basketColor;
    public TMP_Text counterFormsText;
    public bool basketSelected, activated;

    private void OnTriggerEnter(Collider other)
    {
        if (!activated) return;

        GameObject target = other.gameObject;
        InteractableForm interactable = target.GetComponentInParent<InteractableForm>();

        if (!interactable) return;

        if (basketSelected)
        {
            if (!interactable.canUse) return;
            interactable.canUse = false;
            if (interactable.formType == formType && interactable.formColor == formColor)
            {
                GeneratorGame.Instance.audioSource.PlayOneShot(GeneratorGame.Instance.baseInteractable.clips[8], 1f);
                GeneratorGame.Instance.currentCountWin++;
            }
            else
            {
                GeneratorGame.Instance.audioSource.PlayOneShot(GeneratorGame.Instance.baseInteractable.clips[7], 1f);
                GeneratorGame.Instance.currentCounterAttempts--;
                GeneratorGame.Instance.currentCountWin--;
            }
        }
        else
        {
            GeneratorGame.Instance.audioSource.PlayOneShot(GeneratorGame.Instance.baseInteractable.clips[7], 1f);
            GeneratorGame.Instance.currentCounterAttempts--;
            GeneratorGame.Instance.currentCountWin--;
        }
        GeneratorGame.Instance.Attempts();
        counterFormsText.text = GeneratorGame.Instance.currentCountWin.ToString();
        GameObject fatherInteractor = target.transform.parent.gameObject;
        fatherInteractor = fatherInteractor.transform.parent.gameObject;
        StartCoroutine(ReturnInteractableToPool(fatherInteractor, interactable));
    }

    private IEnumerator ReturnInteractableToPool(GameObject obj, InteractableForm interactable)
    {
        yield return new WaitForSeconds(3f);
        Spawner.ReturnObjectToPool(obj);
        interactable.canUse = true;
    }
}


