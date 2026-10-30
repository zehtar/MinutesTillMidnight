using UnityEngine;

public class MagazinGunShop : MonoBehaviour
{
    public GameObject[] weapons;          // Оружие, для которого применяются улучшения
    public GameObject[] blackLockLines;   // Иконки блокировки улучшений (чтобы скрывать после покупки)
    public UImanager uiManage;            // Менеджер для списания монет
    public SaveStatsInMenu stm;           // Скрипт с сохранениями

    private void Start()
    {
        uiManage = FindObjectOfType<UImanager>();
        stm = FindObjectOfType<SaveStatsInMenu>();

        // Применяем уже купленные улучшения при старте
        for (int i = 0; i < stm.magazineCheckedGun.Length; i++)
        {
            if (stm.magazineCheckedGun[i])
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
        if (!stm.magazineCheckedGun[index] && uiManage.coinSpend(cost))
        {
            stm.magazineCheckedGun[index] = true;
            ApplyUpgradeEffect(index);
            blackLockLines[index].SetActive(false);
            stm.SaveAllData();
        }
    }

    private void ApplyUpgradeEffect(int index)
    {
        foreach (var weapon in weapons)
        {
            var stats = weapon.GetComponent<GunPlayerSelection>().weaponStats;

            switch (index)
            {
                case 0:
                    stats.bulletsInMagazine += 2;
                    break;
                case 1:
                    stats.reloadTime -= 0.1f;
                    break;
                case 2:
                    stats.critChance += 10;
                    break;
                case 3:
                    stats.reloadTime -= 0.1f;
                    break;
                case 4:
                    stats.critChance += 20;
                    break;
            }
        }
    }
}
