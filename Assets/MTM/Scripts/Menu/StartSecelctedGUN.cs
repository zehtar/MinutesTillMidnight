using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSecelctedGUN : MonoBehaviour
{
    void Start()
    {
        GunPlayerSelection weaponSelection = GetComponent<GunPlayerSelection>();
        if (weaponSelection != null)
        {
            weaponSelection.SelectWeapon();
        }
    }
}
