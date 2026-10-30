using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGun : MonoBehaviour
{
    public UImanager uiManage;
    public StatsSaved st;
    public SaveStatsInMenu stm;
    public GameObject[] notBuy;
    public GameObject[] upgradeLocks;
    public int[] upgradeCosts = { 250, 500, 1000, 2000 };
    public List<GameObject> gunsStatList;
    public List<GameObject> levelViewImages;
    
    public void Start()
    {
        uiManage = FindObjectOfType<UImanager>();
        st = FindObjectOfType<StatsSaved>();
        stm = FindObjectOfType<SaveStatsInMenu>();
        CheckBuy();
        CheckSaved();
    }

    private void CheckBuy()
    {
        if(stm.buyGunChecked[0]) notBuy[0].SetActive(false);
        if(stm.buyGunChecked[1]) notBuy[1].SetActive(false);
        if(stm.buyGunChecked[2]) notBuy[2].SetActive(false);
        if(stm.buyGunChecked[3]) notBuy[5].SetActive(false);
        if(stm.buyGunChecked[4]) notBuy[6].SetActive(false);
        if(stm.buyPlChecked[0]) notBuy[3].SetActive(false);
        if(stm.buyPlChecked[1]) notBuy[4].SetActive(false);
    }
    
    public void CheckSaved()
    {
        for (int gunIndex = 0; gunIndex < 6; gunIndex++)
        {
            for (int level = 0; level < 4; level++)
            {
                if (GetUpgradeArray(gunIndex)[level])
                {
                    Debug.Log($"Применение улучшения: Оружие {gunIndex}, Уровень {level + 1}");
                    ApplyUpgrade(gunIndex, level + 1);
                }
            }
        }
    }

    public void BuyGunUpgrade(int gunIndex, int level)
    {
        AudioManager.Instance.PlayClick();
        
        if (gunsStatList[gunIndex] == null) return; 

        var weaponStats = gunsStatList[gunIndex].GetComponent<GunPlayerSelection>().weaponStats;

        if (uiManage.coinSpend(upgradeCosts[level - 1]))
        {
            weaponStats.passivesLevel = level; 
            upgradeLocks[gunIndex * 4 + (level - 1)].SetActive(false); 

            // Сохраняем покупку в нужный массив
            GetUpgradeArray(gunIndex)[level - 1] = true;
            stm.SaveAllData();
            ApplyUpgrade(gunIndex,level);
            gunsStatList[gunIndex].GetComponent<GunPlayerSelection>().SelectWeapon();
        }
    }

    private void ApplyUpgrade(int gunIndex, int level)
    {
        upgradeLocks[gunIndex * 4 + (level - 1)].SetActive(false); //  выкл полосок
        levelViewImages[gunIndex * 4 + (level - 1)].SetActive(true); 

        if (gunsStatList[gunIndex] == null)
        {
            Debug.LogError($"Оружие {gunIndex} не найдено!");
            return;
        }

        var weaponStats = gunsStatList[gunIndex].GetComponent<GunPlayerSelection>().weaponStats;
        if (weaponStats == null)
        {
            Debug.LogError($"weaponStats у оружия {gunIndex} = null!");
            return;
        }

       // Debug.Log($"До улучшения: {weaponStats.weaponName}, Уровень пассивок {weaponStats.passivesLevel}");

        switch (gunIndex)
        {
            case 0:
                if (level == 1) weaponStats.penetration += 1;
                if (level == 2) weaponStats.bulletDamage += 1;
                if (level == 3) weaponStats.bulletsInMagazine += 2;
                if (level == 4) weaponStats.spread -= 3;
                break;
            case 1:
                if (level == 1) weaponStats.shotsPerShot += 1;
                if (level == 2) weaponStats.bulletDamage += 1;
                if (level == 3) weaponStats.penetration += 1;
                if (level == 4) weaponStats.fireRate -= 0.2f;
                break;
            case 2:
                if (level == 1) weaponStats.fireRate -= 0.1f;
                if (level == 2) weaponStats.bulletsInMagazine += 5;
                if (level == 3) weaponStats.spread -= 3;
                if (level == 4) weaponStats.fireRate -= 0.05f;
                break;
            case 3:
                if (level == 1) weaponStats.fireRate -= 0.2f;
                if (level == 2) weaponStats.penetration += 3;
                if (level == 3) weaponStats.spread -= 5;
                if (level == 4)
                {
                    weaponStats.spread -= 1;
                    weaponStats.penetration += 2;
                    weaponStats.bulletsInMagazine += 2;
                } 
                break;
            case 4:
                if (level == 1) weaponStats.fireRate -= 0.1f;
                if (level == 2) weaponStats.bulletsInMagazine += 5;
                if (level == 3) weaponStats.spread -= 3;
                if (level == 4) weaponStats.fireRate -= 0.05f;
                break;
            case 5:
                if (level == 1) weaponStats.fireRate -= 0.2f;
                if (level == 2) weaponStats.penetration += 3;
                if (level == 3) weaponStats.spread -= 5;
                if (level == 4)
                {
                    weaponStats.spread -= 1;
                    weaponStats.penetration += 2;
                    weaponStats.bulletsInMagazine += 2;
                }
                break;
        }

        //Debug.Log($"После улучшения: {weaponStats.weaponName}, Новый уровень пассивок {weaponStats.passivesLevel}");
    }

    private bool[] GetUpgradeArray(int gunIndex)
    {
        switch (gunIndex)
        {
            case 0: return stm.upgradeCheckedGun1;
            case 1: return stm.upgradeCheckedGun2;
            case 2: return stm.upgradeCheckedGun3;
            case 3: return stm.upgradeCheckedGun4;
            case 4: return stm.upgradeCheckedGun5;
            case 5: return stm.upgradeCheckedGun6;
            default: return new bool[5];
        }
    }

    public void Gun1_p1() { BuyGunUpgrade(0, 1); }
    public void Gun1_p2() { BuyGunUpgrade(0, 2); }
    public void Gun1_p3() { BuyGunUpgrade(0, 3); }
    public void Gun1_p4() { BuyGunUpgrade(0, 4); }

    public void Gun2_p1() { BuyGunUpgrade(1, 1); }
    public void Gun2_p2() { BuyGunUpgrade(1, 2); }
    public void Gun2_p3() { BuyGunUpgrade(1, 3); }
    public void Gun2_p4() { BuyGunUpgrade(1, 4); }

    public void Gun3_p1() { BuyGunUpgrade(2, 1); }
    public void Gun3_p2() { BuyGunUpgrade(2, 2); }
    public void Gun3_p3() { BuyGunUpgrade(2, 3); }
    public void Gun3_p4() { BuyGunUpgrade(2, 4); }
    public void Gun4_p1() { BuyGunUpgrade(3, 1); }
    public void Gun4_p2() { BuyGunUpgrade(3, 2); }
    public void Gun4_p3() { BuyGunUpgrade(3, 3); }
    public void Gun4_p4() { BuyGunUpgrade(3, 4); }
    public void Gun5_p1() { BuyGunUpgrade(4, 1); }
    public void Gun5_p2() { BuyGunUpgrade(4, 2); }
    public void Gun5_p3() { BuyGunUpgrade(4, 3); }
    public void Gun5_p4() { BuyGunUpgrade(4, 4); }
    public void Gun6_p1() { BuyGunUpgrade(5, 1); }
    public void Gun6_p2() { BuyGunUpgrade(5, 2); }
    public void Gun6_p3() { BuyGunUpgrade(5, 3); }
    public void Gun6_p4() { BuyGunUpgrade(5, 4); }

    public void BuyGun2()   { if(uiManage.coinSpend(300)) { notBuy[0].SetActive(false); stm.buyGunChecked[0] = true; stm.SaveAllData();} }
    public void BuyGun3()   { if(uiManage.coinSpend(500)) { notBuy[1].SetActive(false); stm.buyGunChecked[1] = true; stm.SaveAllData();} }
    public void BuyGun4()   { if(uiManage.coinSpend(1000)) { notBuy[2].SetActive(false); stm.buyGunChecked[2] = true;stm.SaveAllData(); } }
    public void BuyGun5()   { if(uiManage.coinSpend(3000)) { notBuy[5].SetActive(false); stm.buyGunChecked[3] = true;stm.SaveAllData(); } }
    public void BuyGun6()   { if(uiManage.coinSpend(5000)) { notBuy[6].SetActive(false); stm.buyGunChecked[4] = true;stm.SaveAllData(); } }
    public void BuyPlayer2() { if(uiManage.coinSpend(600)) { notBuy[3].SetActive(false); stm.buyPlChecked[0] = true;stm.SaveAllData(); } }
    public void BuyPlayer3() { if(uiManage.coinSpend(800)) { notBuy[4].SetActive(false); stm.buyPlChecked[1] = true;stm.SaveAllData(); } }
}
