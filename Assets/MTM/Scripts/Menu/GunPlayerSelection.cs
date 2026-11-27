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
        
        statsSaved.selectedWeapon = new WeaponStats
        {
            isSuperWeapon = weaponStats.isSuperWeapon,
            weaponName = weaponStats.weaponName,
            fireRate = weaponStats.fireRate,
            shotsPerShot = weaponStats.shotsPerShot,
            bulletSpeed = weaponStats.bulletSpeed,
            bulletDamage = weaponStats.bulletDamage,
            passivesLevel = weaponStats.passivesLevel,
            penetration = weaponStats.penetration,
            bulletsInMagazine = weaponStats.bulletsInMagazine,
            spread = weaponStats.spread,
            reloadTime = weaponStats.reloadTime,
            critChance = weaponStats.critChance,
            critDamage = weaponStats.critDamage,
            knockBackForce = weaponStats.knockBackForce,
            shootClip =  weaponStats.shootClip
        };

        UpdateWeaponStatsUI();
        infomationGunUpgrades.SelectInfoPanel();
    }

    public void DeselectWeapon()
    {
        selectionCircle.sprite = defaultSprite;
    }

    void UpdateWeaponStatsUI()
    {
        if (damageText.text != statsSaved.selectedWeapon.bulletDamage.ToString())
        {
            damageText.text = "" + statsSaved.selectedWeapon.bulletDamage;
            damageText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (fireRateText.text != statsSaved.selectedWeapon.fireRate.ToString())
        {
            fireRateText.text = "" + statsSaved.selectedWeapon.fireRate;
            fireRateText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (bulletSpeedText.text != statsSaved.selectedWeapon.bulletSpeed.ToString())
        {
            bulletSpeedText.text = "" + statsSaved.selectedWeapon.bulletSpeed;
            bulletSpeedText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (shotsPerShotText.text != statsSaved.selectedWeapon.shotsPerShot.ToString())
        {
            shotsPerShotText.text = "" + statsSaved.selectedWeapon.shotsPerShot;
            shotsPerShotText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (bulletInMAgazineText.text != statsSaved.selectedWeapon.bulletsInMagazine.ToString())
        {
            bulletInMAgazineText.text = "" + statsSaved.selectedWeapon.bulletsInMagazine;
            bulletInMAgazineText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (penetrationText.text != statsSaved.selectedWeapon.penetration.ToString())
        {
            penetrationText.text = "" + statsSaved.selectedWeapon.penetration;
            penetrationText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (spreadText.text != statsSaved.selectedWeapon.spread.ToString())
        {
            spreadText.text = "" + statsSaved.selectedWeapon.spread;
            spreadText.GetComponent<Animator>().SetTrigger("updateText");
        }
    }
}

