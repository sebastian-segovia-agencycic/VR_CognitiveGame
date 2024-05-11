using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialsManager : MonoBehaviour
{
    public RoomManager roomManager;
    public List<Transform> listRotation = new List<Transform>();
    public List<float> listMinRanges = new List<float>();
    public List<float> listMaxRanges = new List<float>();
    public bool canOven, canInteract, flag = true;

    public List<bool> boolsOven = new List<bool>();
    public GameObject pizzaOven;

    private void Start()
    {
        boolsOven = roomManager.listBool;
        ActivateGasFire();
    }

    public void CanOven()
    {
        canOven = CheckOven();
        if (canOven && flag)
        {
            flag = false;
            pizzaOven.SetActive(true);
        }
        else if (!canOven && flag)
            pizzaOven.SetActive(false);
    }

    public bool CheckOven()
    {
        foreach (var b in boolsOven)
        {
            if (b == false)
                return false;
        }
        return true;
    }


    public void ActivateGasFire()
    {
        CanInteract();
        var canInteract = this.canInteract;
        if (canInteract)
            roomManager.ActivateBool(0);
        else
            roomManager.DeactivateBool(0);
    }

    public void CanInteract()
    {
        foreach (var element in listRotation)
        {
            for (int i = 0; i < listMinRanges.Count; i++)
            {
                Vector3 eulerRotation = element.rotation.eulerAngles;
                float zRotation = eulerRotation.z;

                if (zRotation >= listMinRanges[i] && zRotation <= listMaxRanges[i])
                    canInteract = true;
                else
                    canInteract = false;
            }
        }
    }
}
