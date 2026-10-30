using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireJpi : MonoBehaviour
{
    public bool isHolding = false;

    public void OnPointerDown()
    {
        isHolding = true;
    }

    public void OnPointerUp()
    {
        isHolding = false;
    }

}
