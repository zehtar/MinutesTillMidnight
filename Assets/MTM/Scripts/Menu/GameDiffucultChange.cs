using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameDiffucultChange : MonoBehaviour
{
    public StatsSaved st;
    public SaveStatsInMenu stm;
    public int diffucultLevel = 1;

    public Text coinText;
    public List<Image> diffucultImages;
    public List<Text> diffucultText;
    public GameObject difficutPanel;
    public GameObject lockLevelImage;
    public int levelId;
    
    public float coinMultiply;
    // идентефикатор уровня
    // 1 ур ничего удивидетнольбн
    // 2 ур у врагов больше хп (30%)
    // 3 ур у врагов больше скорость (+1)
    // 4 ур враги наносят в два раза больше урона
    // 5 ур боссы сильнее (хп x3) 

    // мультиплеер монет
    // 1
    // 1.1
    // 1.2
    // 1.3
    // 1.5  и тут надо сделать какой нибудь ништяк крутой. типо корону на персонажа накинуть хз<>

    void Start()
    {
        st = FindObjectOfType<StatsSaved>();
        stm = FindAnyObjectByType<SaveStatsInMenu>();
        
        if (stm.levelFirstComplite && levelId == 1)
        {
            difficutPanel.SetActive(true);
            lockLevelImage.SetActive(false);
        }
        if (stm.levelSecondComplite && levelId == 2)
        {
            difficutPanel.SetActive(true);
            lockLevelImage.SetActive(false);
        }
        if (stm.levelThirdComplite && levelId == 3)
        {
            difficutPanel.SetActive(true);
        }
    }
    public void DeSelectAll(int i)
    {
        foreach (var texts in diffucultText)
        {
            texts.gameObject.SetActive(false);
        }
        
        for (int j = 0; j < diffucultImages.Count; j++)
        {
            if (j > i)
            {
                diffucultImages[j].gameObject.SetActive(false);
            }
        }
        // 0 1 2 3 4     2    1 1 1 0 0
    }

    public void SelectLevel(int i)
    {
        diffucultImages[i].gameObject.SetActive(true);
        diffucultText[i].gameObject.SetActive(true);
    }

    //damage поправить

    public void SelectDifficult()
    {
        switch (diffucultLevel)
        {
            case 1:
                DeSelectAll(0);
                SelectLevel(0);
                coinMultiply = 1f;
                break;
            case 2:
                DeSelectAll(1);
                SelectLevel(1);
                coinMultiply = 1.1f;
                break;
            case 3:
                DeSelectAll(2);
                SelectLevel(2);
                coinMultiply = 1.2f;
                break;
            case 4:
                DeSelectAll(3);
                SelectLevel(3);
                coinMultiply = 1.3f;
                break;
            case 5:
                DeSelectAll(4);
                SelectLevel(4);
                coinMultiply = 1.5f;
                break;
            default: break;
        }
        coinText.text = coinMultiply.ToString() + 'x';
    }


    public void ApplyDiffucult()
    {
        switch (diffucultLevel)
        {
            case 1:
                st.CoinMyltiply = 1f; break;
            case 2:
                st.enemyLevel = 1;
                st.CoinMyltiply = 1.1f; break;
            case 3:
                st.enemyLevel = 1;
                st.enemySpeed = 1;
                st.CoinMyltiply = 1.2f; break;
            case 4:
                st.enemyDamage = 2;
                st.enemyLevel = 1;
                st.enemySpeed = 1;
                st.CoinMyltiply = 1.3f;  break;
            case 5:
                st.enemyLevel = 1;
                st.enemySpeed = 1;
                st.enemyDamage = 2;
                st.eneeyBossHpMyltiply = 3f;
                st.CoinMyltiply = 1.5f;  break;
            default: break;
        }
    }

    public void DiffBuff()
    {
        if (diffucultLevel < 5)
        {
            diffucultLevel++;
        }
    }

    public void DiffDeBuffs()
    {
        if (diffucultLevel > 1)
        {
            diffucultLevel--;
        }
    }
}