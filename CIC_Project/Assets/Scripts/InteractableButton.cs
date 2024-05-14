using System.Collections;
using System.Collections.Generic;
using Tilia.Interactions.Controllables.LinearDriver;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    private LinearDriveFacade driveFacade;
    public bool playButtom, doOnce, cloneButton;

    void Start()
    {
        driveFacade = GetComponent<LinearDriveFacade>();
    }

    public void ButtonReciver()
    {
        if (cloneButton)
        {
            Debug.Log("Hola");    
        }
        else
        {
            if (doOnce)
            {
                doOnce = false;
                Debug.Log("Hola");
                GeneratorGame.Instance.CalculateFinalGame();
            }
        }
    }
}
