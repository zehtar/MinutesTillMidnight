using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<GameObject> confirmPanels;
    public List<itemShopIdentiti> items;           // элементы магазина (10 штук)
    public List<CharacterSelection> characterStats;    // на кого влияют апгрейды
    public List<GunPlayerSelection> weaponStats;   // оружие, на которое влияют апгрейды
    
    public UImanager uiManage;
    public SaveStatsInMenu stm;
    
    public void Start()
    {
        uiManage = FindObjectOfType<UImanager>();
        stm = FindObjectOfType<SaveStatsInMenu>();

        CheckSaved(); 
    }

    public void DeselectAll()
    {
        foreach (GameObject g in confirmPanels)
            g.SetActive(false);
    }

    private void CheckSaved()
    {
        for (int i = 0; i < items.Count && i < stm.shopItemLevels.Length; i++)
        {
            var item = items[i];
            int savedLevel = stm.shopItemLevels[i];

            savedLevel = Mathf.Clamp(savedLevel, 0, item.maxUpgradeLevel);

            item.upgradeLevel = 0;

            for (int level = 1; level <= savedLevel; level++)
            {
                item.images[level - 1].gameObject.SetActive(true);
                
                item.upgradeLevel++;

                ApplyShopUpgrade(i, level);
            }

            if (item.upgradeLevel < item.maxUpgradeLevel)
                item.upgradeCost.text = item.upgradeCosts[item.upgradeLevel].ToString();
            else
                item.upgradeCost.gameObject.SetActive(false);
        }
    }

    public void BuyShopUpgrade(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= items.Count) return;

        var item = items[itemIndex];

        if (item.upgradeLevel >= item.maxUpgradeLevel)
            return;

        int currentLevel = item.upgradeLevel;            
        int price = item.upgradeCosts[currentLevel];

        if (!uiManage.coinSpend(price))
            return; 

        item.images[currentLevel].gameObject.SetActive(true);

        item.upgradeLevel++;

        if (item.upgradeLevel < item.maxUpgradeLevel)
            item.upgradeCost.text = item.upgradeCosts[item.upgradeLevel].ToString();
        else
            item.upgradeCost.gameObject.SetActive(false);

        ApplyShopUpgrade(itemIndex, item.upgradeLevel);

        stm.shopItemLevels[itemIndex] = item.upgradeLevel;
        stm.SaveAllData();
    }

    private void ApplyShopUpgrade(int itemIndex, int level)
    {
        switch (itemIndex)
        {
            case 0:
                foreach (var weapon in weaponStats)
                {
                    if (weapon != null && weapon.weaponStats != null)
                    {
                        weapon.weaponStats.bulletDamage += 0.5f;
                    }
                }
                break;

            case 1:
                foreach (var weapon in weaponStats)
                {
                    if (weapon != null && weapon.weaponStats != null)
                    {
                        weapon.weaponStats.bulletsInMagazine += 1;
                    }
                }
                break;
            
            case 2:
                foreach (var weapon in weaponStats)
                {
                    if (weapon != null && weapon.weaponStats != null)
                    {
                        weapon.weaponStats.spread -= 1;
                    }
                }
                break;
            
            case 3:
                foreach (var weapon in weaponStats)
                {
                    if (weapon != null && weapon.weaponStats != null)
                    {
                        weapon.weaponStats.shotsPerShot += 1;
                    }
                }
                break;
            case 4:
                foreach (var weapon in weaponStats)
                {
                    if (weapon != null && weapon.weaponStats != null)
                    {
                        weapon.weaponStats.fireRate -= 0.05f;
                    }
                }
                break;
            
            case 5:
                foreach (var player in characterStats)
                {
                    if (player != null && player.characterStats != null)
                    {
                        player.characterStats.health += 1;
                    }
                }
                break;

            case 6:
                foreach (var player in characterStats)
                {
                    if (player != null && player.characterStats != null)
                    {
                        player.characterStats.speed += 0.5f;
                    }
                }
                break;
            
            case 7:
                foreach (var weapon in weaponStats)
                {
                    if (weapon != null && weapon.weaponStats != null)
                    {
                        weapon.weaponStats.reloadTime -= 0.05f;
                    }
                }
                break;
            
            case 8:
                foreach (var weapon in weaponStats)
                {
                    if (weapon != null && weapon.weaponStats != null)
                    {
                        weapon.weaponStats.bulletSpeed += 3;
                    }
                }
                break;
            
            case 9:
                foreach (var player in characterStats)
                {
                    if (player != null && player.characterStats != null)
                    {
                        player.characterStats.collectRange += 0.05f;
                    }
                }
                break;
        }
    }

    public void Item0_Buy() { BuyShopUpgrade(0); }
    public void Item1_Buy() { BuyShopUpgrade(1); }
    public void Item2_Buy() { BuyShopUpgrade(2); }
    public void Item3_Buy() { BuyShopUpgrade(3); }
    public void Item4_Buy() { BuyShopUpgrade(4); }
    public void Item5_Buy() { BuyShopUpgrade(5); }
    public void Item6_Buy() { BuyShopUpgrade(6); }
    public void Item7_Buy() { BuyShopUpgrade(7); }
    public void Item8_Buy() { BuyShopUpgrade(8); }
    public void Item9_Buy() { BuyShopUpgrade(9); }
}