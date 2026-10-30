using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnableDisable : MonoBehaviour
{
    public FireScript fireScript;
    void Start()
    {
        fireScript = FindObjectOfType<FireScript>();
    }

    public void EnableFire()
    {
        if (fireScript == null)
        {
            fireScript = FindObjectOfType<FireScript>();
        }

        fireScript.canfire = true;
    }

    public void DisableFire()
    {
        if (fireScript == null)
        {
            fireScript = FindObjectOfType<FireScript>();
        }

        fireScript.canfire = false;
    }
}
