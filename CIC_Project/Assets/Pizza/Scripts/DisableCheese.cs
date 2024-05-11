using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCheese : MonoBehaviour
{
    public Collider cheese;

    public void DeactivateCheese()
    {
        cheese.enabled = false;
    }
}
