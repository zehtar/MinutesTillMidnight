using UnityEngine;

public class MagazinPlayerShop : MonoBehaviour
{
    public GameObject[] characters;       // Все персонажи
    public GameObject[] blackLockLines;   // Иконки блокировки улучшений
    public UImanager uiManage;             // Менеджер монет
    public SaveStatsInMenu stm;            // Скрипт с сохранением прогресса

    private void Start()
    {
        uiManage = FindObjectOfType<UImanager>();
        stm = FindObjectOfType<SaveStatsInMenu>();

        // Применяем уже купленные улучшения при старте
        for (int i = 0; i < stm.magazineCheckedPlayer.Length; i++)
        {
            if (stm.magazineCheckedPlayer[i])
            {
                blackLockLines[i].SetActive(false);
                ApplyUpgradeEffect(i);
            }
        }
    }

    public void Upgrade_1()
    {
        BuyUpgrade(0, 500);
    }

    public void Upgrade_2()
    {
        BuyUpgrade(1, 800);
    }

    public void Upgrade_3()
    {
        BuyUpgrade(2, 800);
    }

    public void Upgrade_4()
    {
        BuyUpgrade(3, 1000);
    }

    public void Upgrade_5()
    {
        BuyUpgrade(4, 1000);
    }

    private void BuyUpgrade(int index, int cost)
    {
        if (!stm.magazineCheckedPlayer[index] && uiManage.coinSpend(cost))
        {
            stm.magazineCheckedPlayer[index] = true;
            ApplyUpgradeEffect(index);
            blackLockLines[index].SetActive(false);
            stm.SaveAllData();
        }
    }

    private void ApplyUpgradeEffect(int index)
    {
        foreach (var character in characters)
        {
            var stats = character.GetComponent<CharacterSelection>().characterStats;

            switch (index)
            {
                case 0: // +0.5 к бегу
                    stats.speed += 0.5f;
                    break;
                case 1: // реген 1 HP в 60 сек
                    stats.regenerationRate = 60f;
                    break;
                case 2: // +10% к дальности сбора
                    stats.collectRange += 0.1f;
                    break;
                case 3: // реген 1 HP в 30 сек
                    stats.regenerationRate = 30f;
                    break;
                case 4: // +20% к дальности сбора
                    stats.collectRange += 0.2f;
                    break;
            }
        }
    }
}
