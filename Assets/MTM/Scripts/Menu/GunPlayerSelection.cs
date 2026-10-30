using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPlayerSelection : MonoBehaviour
{
    public Image selectionCircle; // UI-спрайт выбора
    public Sprite selectedSprite; // Активный спрайт
    public Sprite defaultSprite; // Обычный спрайт
    public string weaponName; // Имя оружия (локальное назначается в инспекторе)

    public Text damageText;
    public Text fireRateText;
    public Text bulletSpeedText;
    public Text shotsPerShotText;
    public Text bulletInMAgazineText;
    public Text penetrationText;
    public Text spreadText;

    public StatsSaved statsSaved;

    // Привязываем оружие через инспектор
    public WeaponStats weaponStats;

    private static GunPlayerSelection selectedWeapon; // Текущая выбранная кнопка

    public InfomationGunUpgrades infomationGunUpgrades;

    public void Awake()
    {
        statsSaved = FindObjectOfType<StatsSaved>();
        infomationGunUpgrades = FindAnyObjectByType<InfomationGunUpgrades>();
    }

    public void SelectWeapon()
    {
         if (selectedWeapon != null && selectedWeapon != this)
        {
            selectedWeapon.DeselectWeapon();
        }

        selectionCircle.sprite = selectedSprite;
        selectedWeapon = this;

        statsSaved.spread = weaponStats.spread;
        statsSaved.bulletsInMagazine = weaponStats.bulletsInMagazine;
        statsSaved.gunSelected = weaponStats.weaponName;
        statsSaved.bulletdamage = weaponStats.bulletDamage;
        statsSaved.gunfireRate = weaponStats.fireRate;
        statsSaved.bulletspeed = weaponStats.bulletSpeed;
        statsSaved.gunshotPerShot = weaponStats.shotsPerShot;
        statsSaved.bulletsInMagazine = weaponStats.bulletsInMagazine;
        statsSaved.passivesLevel = weaponStats.passivesLevel;
        statsSaved.penetration = weaponStats.penetration;
        statsSaved.reloadTime = weaponStats.reloadTime;
        statsSaved.critChance = weaponStats.critChance;
        statsSaved.critDamage = weaponStats.critDamage;
        statsSaved.isSuperWeapon = weaponStats.isSuperWeapon;

        UpdateWeaponStatsUI();
        infomationGunUpgrades.SelectInfoPanel();
    }

    public void DeselectWeapon()
    {
        selectionCircle.sprite = defaultSprite;
    }

    void UpdateWeaponStatsUI()
    {
        if (damageText.text != statsSaved.bulletdamage.ToString())
        {
            damageText.text = "" + statsSaved.bulletdamage;
            damageText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (fireRateText.text != statsSaved.gunfireRate.ToString())
        {
            fireRateText.text = "" + statsSaved.gunfireRate;
            fireRateText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (bulletSpeedText.text != statsSaved.bulletspeed.ToString())
        {
            bulletSpeedText.text = "" + statsSaved.bulletspeed;
            bulletSpeedText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (shotsPerShotText.text != statsSaved.gunshotPerShot.ToString())
        {
            shotsPerShotText.text = "" + statsSaved.gunshotPerShot;
            shotsPerShotText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (bulletInMAgazineText.text != statsSaved.bulletsInMagazine.ToString())
        {
            bulletInMAgazineText.text = "" + statsSaved.bulletsInMagazine;
            bulletInMAgazineText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (penetrationText.text != statsSaved.penetration.ToString())
        {
            penetrationText.text = "" + statsSaved.penetration;
            penetrationText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (spreadText.text != statsSaved.spread.ToString())
        {
            spreadText.text = "" + statsSaved.spread;
            spreadText.GetComponent<Animator>().SetTrigger("updateText");
        }
    }
}

