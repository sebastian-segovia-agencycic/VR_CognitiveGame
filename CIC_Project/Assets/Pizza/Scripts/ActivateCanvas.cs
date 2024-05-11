using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCanvas : MonoBehaviour
{
    public GameObject panel;
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;

        if (target.CompareTag("Client"))
        {
            panel.SetActive(true);
        }
    }
}
