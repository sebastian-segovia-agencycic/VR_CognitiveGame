using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<bool> listBool = new List<bool>();

    public void ActivateBool(int indexBool)
    {
        listBool[indexBool] = true;
    }

    public void DeactivateBool(int indexBool)
    {
        listBool[indexBool] = false;
    }
}
