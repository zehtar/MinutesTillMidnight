using System.Collections.Generic;
using UnityEngine;

public class SkinsGunBuy : MonoBehaviour
{
    public List<GunPlayerSelection> guns;
    public List<GameObject> buttons;
    public List<GameObject> info;
    public UImanager uiManager;
    public SaveStatsInMenu stm;
    

    private void Start()
    {
        uiManager = FindObjectOfType<UImanager>();
        stm = FindObjectOfType<SaveStatsInMenu>();
        
        LoadSkins();
    }
    
    private void LoadSkins()
    {
        for (int i = 0; i < stm.magazineSkins.Length; i++)
        {
            if (stm.magazineSkins[i])
            {
                buttons[i].SetActive(false);
                info[i].SetActive(true);
                switch (i)
                {
                    case 0: EquipGun1(); break;
                    case 1: EquipGun2(); break;
                    case 2: EquipGun3(); break;
                    case 3: EquipGun4(); break;
                    case 4: EquipGun5(); break;
                    case 5: EquipGun6(); break;
                    default: break;
                }
            }
        }
    }

    private void BuySkin(int index)
    {
        if (uiManager.coinSpend(5000))
        {
            stm.magazineSkins[index] = true;
            buttons[index].SetActive(false);
            info[index].SetActive(true);
            stm.SaveAllData();
            switch (index)
            {
                case 0: EquipGun1(); break;
                case 1: EquipGun2(); break;
                case 2: EquipGun3(); break;
                case 3: EquipGun4(); break;
                case 4: EquipGun5(); break;
                case 5: EquipGun6(); break;
                default: break;
            }
        }
    }

    public void BuyGun1() { BuySkin(0); }
    public void BuyGun2() { BuySkin(1); }
    public void BuyGun3() { BuySkin(2); }
    public void BuyGun4() { BuySkin(3); }
    
    public void BuyGun5() { BuySkin(4); }
    public void BuyGun6() { BuySkin(5); }

    public void EquipGun1() { guns[0].weaponStats.isSuperWeapon = true; guns[0].SelectWeapon();}
    public void EquipGun2() { guns[1].weaponStats.isSuperWeapon = true; guns[1].SelectWeapon();}
    public void EquipGun3() { guns[2].weaponStats.isSuperWeapon = true; guns[2].SelectWeapon();}
    public void EquipGun4() { guns[3].weaponStats.isSuperWeapon = true; guns[3].SelectWeapon();}
    
    public void EquipGun5() { guns[4].weaponStats.isSuperWeapon = true; guns[4].SelectWeapon();}
    public void EquipGun6() { guns[5].weaponStats.isSuperWeapon = true; guns[5].SelectWeapon();}
   
}
