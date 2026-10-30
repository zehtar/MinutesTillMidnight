using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPassivesActive : MonoBehaviour
{
    public StatsSaved st;
    public int passivesLevel;
    public string gunName;
    //public int thisGun;
    public PowerUpsScript pu;

    public bool critChanceActive = false;
    public bool bigAmmo = false;
    public bool fireSpeeder = false;

    void Start()
    {
        st = FindObjectOfType<StatsSaved>();  
        //thisGun = FindObjectOfType<FireScript>();  
        //pu = FindObjectOfType<PowerUpsScript>();

        passivesLevel = st.passivesLevel;
        gunName = st.gunSelected;
        
        if (st.isSuperWeapon)
        {
            switch (gunName)
            {
                case "Gun1":
                    critChanceActive = true;
                    break;
                case "Gun2":
                    bigAmmo = true;
                    break;
                case "Gun3":
                    fireSpeeder = true;
                    break;
                default: Debug.Log("у меня какой?  " + gunName); break;
            }
        }
    }

    
   
}
