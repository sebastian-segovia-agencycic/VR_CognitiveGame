using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public RoomManager roomManager;
    public List<bool> boolsWinner = new List<bool>();
    public bool canWin;
    public GameObject panelWinner;

    void Start()
    {
        boolsWinner = roomManager.listBool;
    }

    public void CanWin()
    {
        boolsWinner = roomManager.listBool;
        canWin = CheckWinner();
        if (canWin)
            panelWinner.SetActive(true);
        else
            panelWinner.SetActive(false);
    }

    public bool CheckWinner()
    {
        foreach (var b in boolsWinner)
        {
            if (b == false)
                return false;
        }
        return true;
    }

}
