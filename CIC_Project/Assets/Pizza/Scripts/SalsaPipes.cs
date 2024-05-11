using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalsaPipes : MonoBehaviour
{
    public RoomManager roomManager;
    public bool canSalsa;
    public List<bool> boolsalsa = new List<bool>();
    public GameObject salsa;

    private void Start()
    {
        boolsalsa = roomManager.listBool;
    }

    public void EnableSalsa()
    {
        boolsalsa = roomManager.listBool;
        canSalsa = CheckSalsa();
        if (canSalsa)
        {
            salsa.SetActive(true);
            var animator = salsa.GetComponent<Animator>();
            animator.SetBool("Salsa", true);
        }
    }

    private bool CheckSalsa()
    {
        for (int i = 0; i < 3; i++)
        {
            if (boolsalsa[i] == false)
                return false;
        }
        return true;
    }
}
