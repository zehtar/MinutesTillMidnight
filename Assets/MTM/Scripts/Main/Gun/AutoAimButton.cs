using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimButton : MonoBehaviour
{
    public WeaponControl wp;
    void Start()
    {
        wp = FindAnyObjectByType<WeaponControl>();
    }
    
    public void Click()
    {
        if (wp == null)
        {
            wp = FindAnyObjectByType<WeaponControl>();
        }

        wp.AutoAimButton();
    }
}
