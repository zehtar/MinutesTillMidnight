using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuyPlayer : MonoBehaviour
{
    public UImanager uiManage;
    public StatsSaved st;
    public SaveStatsInMenu stm;
    public GameObject[] notBuy;
    public GameObject[] upgradeLocks;
    public int[] upgradeCosts = { 100, 200, 300, 400 };
    public List<GameObject> playerStatList;  
    public List<GameObject> levelViewImages;

    public void Start()
    {
        uiManage = FindObjectOfType<UImanager>();
        st = FindObjectOfType<StatsSaved>();
        stm = FindObjectOfType<SaveStatsInMenu>();
        CheckSaved();
    }
    
    public void CheckSaved()
    {
        for (int index = 0; index < 4; index++)
        {
            for (int level = 0; level < 4; level++)
            {
                if (GetUpgradeArray(index)[level])
                {
                    Debug.Log($"Применение улучшения: Игрок {index}, Уровень {level + 1}");
                    ApplyUpgrade(index, level + 1);
                }
            }
        }
    }

    public void BuyGunUpgrade(int playerIndex, int level)
    {
        AudioManager.Instance.PlayClick();
        
        if (playerStatList[playerIndex] == null) return; 
    
        var playerStats = playerStatList[playerIndex].GetComponent<CharacterSelection>().characterStats;

        if (uiManage.coinSpend(upgradeCosts[level - 1]))
        {
            playerStats.passiveslevel = level; 
            upgradeLocks[playerIndex * 4 + (level - 1)].SetActive(false); 

            // Сохраняем покупку в нужный массив
            GetUpgradeArray(playerIndex)[level - 1] = true;
            stm.SaveAllData();
            ApplyUpgrade(playerIndex,level);
            playerStatList[playerIndex].GetComponent<CharacterSelection>().SelectCharacter();
        }
    }

    private void ApplyUpgrade(int playerIndex, int level)
    {
        upgradeLocks[playerIndex * 4 + (level - 1)].SetActive(false); //  выкл полосок
        levelViewImages[playerIndex * 4 + (level - 1)].SetActive(true); 
        
        var playerStats = playerStatList[playerIndex].GetComponent<CharacterSelection>().characterStats;

        switch (playerIndex)
        {
            case 0:
                if (level == 1) playerStats.health += 1;
                if (level == 2) playerStats.speed += 1;
                if (level == 3) playerStats.collectRange += 0.15f;
                if (level == 4) playerStats.collectRange += 0.25f;
                break;
            case 1:
                if (level == 1) playerStats.speed += 1;
                if (level == 2) playerStats.powersDamage += 2;
                if (level == 3) playerStats.powersSize += 1;
                if (level == 4) playerStats.powersDuration += 2f;
                break;
            case 2:
                if (level == 1) playerStats.health += 1;
                if (level == 2) playerStats.speed += 1;
                if (level == 3) playerStats.collectRange += 0.10f;
                if (level == 4)
                {
                    playerStats.health += 1;
                    playerStats.collectRange += 0.10f;
                }
                break;
        }
    }

    private bool[] GetUpgradeArray(int gunIndex)
    {
        switch (gunIndex)
        {
            case 0: return stm.upgradeCheckePlayer1;
            case 1: return stm.upgradeCheckePlayer2;
            case 2: return stm.upgradeCheckePlayer3;
            default: return new bool[5];
        }
    }
    
    public void Player1_p1() { BuyGunUpgrade(0, 1); }
    public void Player1_p2() { BuyGunUpgrade(0, 2); }
    public void Player1_p3() { BuyGunUpgrade(0, 3); }
    public void Player1_p4() { BuyGunUpgrade(0, 4); }
    
    public void Player2_p1() { BuyGunUpgrade(1, 1); }
    public void Player2_p2() { BuyGunUpgrade(1, 2); }
    public void Player2_p3() { BuyGunUpgrade(1, 3); }
    public void Player2_p4() { BuyGunUpgrade(1, 4); }
    
    public void Player3_p1() { BuyGunUpgrade(2, 1); }
    public void Player3_p2() { BuyGunUpgrade(2, 2); }
    public void Player3_p3() { BuyGunUpgrade(2, 3); }
    public void Player3_p4() { BuyGunUpgrade(2, 4); }
    
}
