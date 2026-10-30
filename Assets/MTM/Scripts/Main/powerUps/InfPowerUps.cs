using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class InfPowerUps : MonoBehaviour
{
    public GameObject powerUpPamel;
    public PowerUpsPoulManager _powerUpPamel;
    public FireScript GunFire;
    public BulletPoulManager bulletPoulManager;
    public List<PowerUpIdent> pUList;
    public List<PowerUpIdent> pUBossList;
    public ReloadMove reload;
    public WeaponControl weaponControl;
    public EffectsStatsController effStController;
    
    public SummonTowerEffector summonTowerEffector;
    public BladeEffect  bladeEffect;

    public GameObject usedPUPrefab;
    public Transform usedPUContainer;

    public GameObject healCoin;
    
    public void Start()
    {
        effStController = FindAnyObjectByType<EffectsStatsController>();
        GunFire = FindObjectOfType<FireScript>();
        _powerUpPamel = FindObjectOfType<PowerUpsPoulManager>();
        bulletPoulManager = FindObjectOfType<BulletPoulManager>();
        // reload = FindObjectOfType<ReloadMove>();
        weaponControl = FindObjectOfType<WeaponControl>();
        summonTowerEffector =  FindObjectOfType<SummonTowerEffector>();
        bladeEffect =  FindObjectOfType<BladeEffect>();
    }

   // --- МЕТОД ДЛЯ ПЕРЕВОДА ---
   private string GetLoc(string ru, string en)
   {
       if (YG2.lang == "ru") return ru;
       else return en;
   }
   // -------------------------

   public void ExitFromPanel()
   {
        powerUpPamel.SetActive(false);
        _powerUpPamel.ConfirmCleaner();
        _powerUpPamel.RandoomSummon();
        AudioManager.Instance.PlayClick();
        Time.timeScale = 1;
   }

   public void AddUsedPU(Sprite icon = null, string text = null)
   {
       GameObject clone = Instantiate(usedPUPrefab, usedPUContainer);
       var view = clone.GetComponent<PowerUpButtonViev>();
       view.SetData(icon, text);
   }

   public void HealPowerUp()
   {
       Instantiate(healCoin, transform.position, Quaternion.identity);
       ExitFromPanel();
   }

   //БОСС дропы
    // + 150% к точности, -25% к скорострельности 
    // + 50% к скорострельности, + 12 пуль в магазине,пуль на вылет + 1, - 25% к точности
    // + 40% к размеру пуль, + 60% к урону пуль, + 3 к пронзанию пули, -5% магазина, - 50% к скорострельности
    // + 5 к пронзанию пуль, +30% к урону пуль, пуль на вылет +3, -50% к размеру пуль
    // + 3 крутящихься меча, -40% к урону от пуль
    public void Boss_pU_1_1()
    {
        GunFire.puSpreadBuff(150f);
        GunFire.puFireSpeedBuff(-25f);
        ExitFromPanel();
        Image iconImage = pUBossList[0].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " + 150% к точности, -25% к скорострельности ",
            " + 150% Accuracy, -25% Fire Rate "
        ));
    }
    
    public void Boss_pU_1_2()
    {
        GunFire.puFireSpeedBuff(50f);
        GunFire.puBulletMaxBuff(12);  
        GunFire.puShotPerShot(1);
        GunFire.puSpreadBuff(-25f);
        ExitFromPanel();
        Image iconImage = pUBossList[1].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " + 50% к скорострельности, + 12 пуль в магазине, пуль на вылет + 1, - 25% к точности",
            " + 50% Fire Rate, + 12 Max Ammo, + 1 Projectile per shot, - 25% Accuracy"
        ));
    }
    
    public void Boss_pU_1_3()
    {
        bulletPoulManager.puScale(0.4f);
        bulletPoulManager.puDamagePerchent(60f);
        bulletPoulManager.puPenetrationBuff(3);
        GunFire.puBulletMaxBuff(-5);  
        GunFire.puFireSpeedBuff(-50f);
        ExitFromPanel();
        Image iconImage = pUBossList[2].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " + 40% к размеру пуль, + 60% к урону пуль, + 3 к пронзанию пули, -5% магазина, - 50% к скорострельности",
            " + 40% Bullet Size, + 60% Bullet Damage, + 3 Penetration, -5% Max Ammo, - 50% Fire Rate"
        ));
    }
    
    public void Boss_pU_1_4()
    {
        bulletPoulManager.puPenetrationBuff(5);
        bulletPoulManager.puDamagePerchent(30f);
        GunFire.puShotPerShot(3);
        bulletPoulManager.puScale(-0.5f);
        ExitFromPanel();
        Image iconImage = pUBossList[3].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+ 5 к пронзанию пуль, +30% к урону пуль, пуль на вылет +3, -50% к размеру пуль",
            "+ 5 Penetration, +30% Bullet Damage, +3 Projectiles per shot, -50% Bullet Size"
        ));
    }
    
    public void Boss_pU_1_5()
    {
        bulletPoulManager.puDamagePerchent(-40f);
        ExitFromPanel();
        Image iconImage = pUBossList[4].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+ 3 крутящихься меча, -40% к урону от пуль",
            "+ 3 Spinning Blades, -40% Bullet Damage"
        ));
        summonTowerEffector.BladeEnable();
        bladeEffect.bladesCount += 3;
        bladeEffect.InitializeBlades();
    }

    //основной калибр
    //+ 25% урона от пуль
    //+ 1 патрон на вылет  -15% урона -40% к точности
    //+ 10% скорострельности + 10% урона
    //+ 10% урона + 10% скорострельности 
    //+ 2 патрона на вылет, +15% к скорострельности, -40% к точности
    public void pU_1_1()
    {       
        pUList[1].canBeApleed = true; 
        pUList[0].canBeApleed = false;             
        bulletPoulManager.puDamagePerchent(25f);
        ExitFromPanel();
        Image iconImage = pUList[0].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+ 25% урона от пуль",
            "+ 25% Bullet Damage"
        ));
    }
    
    public void pU_1_2()
    {       
        pUList[2].canBeApleed = true; 
        pUList[1].canBeApleed = false;      
        GunFire.puShotPerShot(1);
        bulletPoulManager.puDamagePerchent(-15f);
        GunFire.puSpreadBuff(-40f);
        ExitFromPanel();
        Image iconImage = pUList[1].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+ 1 патрон на вылет  -15% урона -40% к точности",
            "+ 1 Projectile per shot, -15% Damage, -40% Accuracy"
        ));
    }
      
    public void pU_1_3()
    {        
        pUList[3].canBeApleed = true;  
        pUList[2].canBeApleed = false;      
        bulletPoulManager.puDamagePerchent(10f);
        GunFire.puFireSpeedBuff(10f);
        ExitFromPanel();
        Image iconImage = pUList[2].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+ 10% скорострельности + 10% урона",
            "+ 10% Fire Rate, + 10% Damage"
        ));
    } 

    public void pU_1_4()
    {        
        pUList[4].canBeApleed = true; 
        pUList[3].canBeApleed = false;   
        bulletPoulManager.puDamagePerchent(10f);
        GunFire.puFireSpeedBuff(10f);
        ExitFromPanel();
        Image iconImage = pUList[3].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+ 10% скорострельности + 10% урона",
            "+ 10% Fire Rate, + 10% Damage"
        ));
    } 

    public void pU_1_5()
    {   
        pUList[4].canBeApleed = false;
        pUList[0].canBeApleed = true;
        GunFire.puFireSpeedBuff(15f);
        GunFire.puShotPerShot(2);
        GunFire.puSpreadBuff(-40f);
        ExitFromPanel();
        Image iconImage = pUList[4].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+ 2 патрона на вылет, +15% к скорострельности, -40% к точности",
            "+ 2 Projectiles per shot, +15% Fire Rate, -40% Accuracy"
        ));
    } 
    //перезарядка
    // +10% к скорости перезарядки
    // +2 к пуль в обоиме
    // +15% к скорости перезарядки
    // +4 к пуль в обоиме
    // +15% к скорости перезарядки и +2 к обоиме
    public void pU_2_1()
    {       
        pUList[6].canBeApleed = true;  
        pUList[5].canBeApleed = false;
        reload.puCouldownBuff(10f);      
        ExitFromPanel();
        Image iconImage = pUList[5].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " +10% к скорости перезарядки",
            " +10% Reload Speed"
        ));
    }

    public void pU_2_2()
    {        
        pUList[7].canBeApleed = true;  
        pUList[6].canBeApleed = false;      
        GunFire.puBulletMaxBuff(2);        
        ExitFromPanel();
        Image iconImage = pUList[6].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+2 к пуль в обоиме",
            "+2 Max Ammo"
        ));
    }

    public void pU_2_3()
    {        
        pUList[8].canBeApleed = true;     
        pUList[7].canBeApleed = false;   
        reload.puCouldownBuff(15f);    
        ExitFromPanel();
        Image iconImage = pUList[7].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+15% к скорости перезарядки",
            "+15% Reload Speed"
        ));
    }

    public void pU_2_4()
    {
        pUList[9].canBeApleed = true;  
        pUList[8].canBeApleed = false;  
        GunFire.puBulletMaxBuff(4);        
        ExitFromPanel();
        Image iconImage = pUList[8].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+4 к пуль в обоиме",
            "+4 Max Ammo"
        ));
    }

    public void pU_2_5()
    {   
        pUList[9].canBeApleed = false;
        pUList[5].canBeApleed = true;
        reload.puCouldownBuff(15f);      
        GunFire.puBulletMaxBuff(2);        
        ExitFromPanel();
        Image iconImage = pUList[9].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " +15% к скорости перезарядки и +2 к обоиме",
            " +15% Reload Speed and +2 Max Ammo"
        ));
    }
    //прицел
    // +20% к точности
    // +15% к скорости пули
    // +30% к точности
    // +15% к скорости пули
    // автоприцел +20% к точности +15% к скорости пули
    public void pU_3_1()
    {
        pUList[11].canBeApleed = true;   
        pUList[10].canBeApleed = false;     
        GunFire.puSpreadBuff(20f);
        ExitFromPanel();
        Image iconImage = pUList[10].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " +20% к точности",
            " +20% Accuracy"
        ));
    }

    public void pU_3_2()
    {
        pUList[12].canBeApleed = true;        
        pUList[11].canBeApleed = false;
        bulletPoulManager.puSpeedPerchent(15f);
        ExitFromPanel();
        Image iconImage = pUList[11].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+15% к скорости пули",
            "+15% Bullet Speed"
        ));
    }

    public void pU_3_3()
    {
        pUList[13].canBeApleed = true;       
        pUList[12].canBeApleed = false; 
        GunFire.puSpreadBuff(30f);
        ExitFromPanel();
        Image iconImage = pUList[12].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+30% к точности",
            "+30% Accuracy"
        ));
    }

    public void pU_3_4()
    {
        pUList[14].canBeApleed = true;       
        pUList[13].canBeApleed = false; 
        bulletPoulManager.puSpeedPerchent(15f);
        ExitFromPanel();
        Image iconImage = pUList[13].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+15% к скорости пули",
            "+15% Bullet Speed"
        ));
    }

    public void pU_3_5()
    {
        pUList[14].canBeApleed = false;
        pUList[10].canBeApleed = true;
        weaponControl.puAutoAim();
        bulletPoulManager.puSpeedPerchent(15f);
        GunFire.puSpreadBuff(20f);
        ExitFromPanel();
        Image iconImage = pUList[14].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "автоприцел +20% к точности +15% к скорости пули",
            "Auto-aim, +20% Accuracy, +15% Bullet Speed"
        ));
    }

    // пули
    // +0.1 размер +5% урона от пуль
    // +1 к пронзанию пули
    // +0.1 размер +10% урона от пуль
    // +15% к скорости пули
    // +0.1 размер +15% урона от пуль
    public void pU_4_1()
    {
        pUList[16].canBeApleed = true;       
        pUList[15].canBeApleed = false; 
        bulletPoulManager.puScale(0.1f);
        bulletPoulManager.puDamagePerchent(5f);
        ExitFromPanel();
        Image iconImage = pUList[15].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+0.1 размер +5% урона от пуль",
            "+0.1 Size, +5% Bullet Damage"
        ));
    }

    public void pU_4_2()
    {
        pUList[16].canBeApleed = false;
        pUList[17].canBeApleed = true;        
        bulletPoulManager.puPenetrationBuff(1);
        ExitFromPanel();
        Image iconImage = pUList[16].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+1 к пронзанию пули",
            "+1 Penetration"
        ));
    }

    public void pU_4_3()
    {
        pUList[18].canBeApleed = true;   
        pUList[17].canBeApleed = false;     
        bulletPoulManager.puScale(0.1f);
        bulletPoulManager.puDamagePerchent(10f);
        ExitFromPanel();
        Image iconImage = pUList[17].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+0.1 размер +10% урона от пуль",
            "+0.1 Size, +10% Bullet Damage"
        ));
    }

    public void pU_4_4()
    {
        pUList[19].canBeApleed = true;        
        pUList[18].canBeApleed = false;
        bulletPoulManager.puSpeedPerchent(15f);
        ExitFromPanel();
        Image iconImage = pUList[18].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+15% к скорости пули",
            "+15% Bullet Speed"
        ));
    }

    public void pU_4_5()
    {    
        pUList[19].canBeApleed = false;
        pUList[15].canBeApleed = true;
        bulletPoulManager.puScale(0.1f);
        bulletPoulManager.puDamagePerchent(15f);
        ExitFromPanel();
        Image iconImage = pUList[19].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " +0.1 размер +15% урона от пуль",
            " +0.1 Size, +15% Bullet Damage"
        ));
    }
    //заморозка
    //  вкл эффект заморозки 1сек 30%
    //  +1 к пронзание пули
    //  враги теряют по 15% от макс хп от заморозки
    //  замедление длится на 2 сек больше
    //  урон по замороженным +50%
    public void pU_5_1()
    {
        pUList[20].canBeApleed = false;
        pUList[21].canBeApleed = true;
        if (!GunFire.activeEffects.Contains("IcePenetration"))
        {
            GunFire.activeEffects.Add("IcePenetration");
        }
        ExitFromPanel();
        Image iconImage = pUList[20].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " вкл эффект заморозки 1сек 30%",
            " Enable freeze effect (1 sec, 30%)"
        ));
    }
    public void pU_5_2()
    {
        pUList[21].canBeApleed = false;
        pUList[22].canBeApleed = true;
        bulletPoulManager.puPenetrationBuff(1);
        bulletPoulManager.puDamagePerchent(20f);
        ExitFromPanel();
        Image iconImage = pUList[21].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " +1 к пронзание пули",
            " +1 Penetration"
        ));
    }
    public void pU_5_3()
    {
        pUList[22].canBeApleed = false;
        pUList[23].canBeApleed = true;
        effStController.isSlowHpEffect = true;
        ExitFromPanel();
        Image iconImage = pUList[22].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " враги теряют по 15% от макс хп от заморозки",
            " Enemies lose 15% Max HP from freeze"
        ));
    }
    public void pU_5_4()
    {
        pUList[23].canBeApleed = false;
        pUList[24].canBeApleed = true;
        effStController.slowDuration = 2f;
        ExitFromPanel();
        Image iconImage = pUList[23].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "замедление длится на 2 сек больше",
            "Slow duration increased by 2 sec"
        ));
    }
    public void pU_5_5()
    {
        pUList[24].canBeApleed = false;
        pUList[20].canBeApleed = true;
        effStController.slowDamageUpEffect = true;
        ExitFromPanel();
        Image iconImage = pUList[24].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " урон по замороженным +50%",
            " Damage to frozen enemies +50%"
        ));
    }
    //горение
    //вкл горения при попадании 5 урон в сек 2 сек длительность 
    //вкл огн ауру при смерти врага, +15% к скорострельности 
    // 15% к скорострельности, +35% к урону от пуль
    // +2 сек горения, +3 урона горения, каждый 5 выстрел запускает фаербол в случайном направлении, фаербол при смерти вызывает мощьную ауру смерти врага
    // +3 урона, при полседнем выстреле запускает огненный вихрь около игрока
    public void pU_6_1()
    {
        pUList[25].canBeApleed = false;
        pUList[26].canBeApleed = true;
        if (!GunFire.activeEffects.Contains("Burn"))
        {
            GunFire.activeEffects.Add("Burn");
        }
        effStController.burnDamage += 5f;
        ExitFromPanel();
        Image iconImage = pUList[25].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "вкл горения при попадании 5 урон в сек 2 сек длительность ",
            "Enable burn on hit (5 dmg/sec, 2 sec duration)"
        ));
    }
    public void pU_6_2()
    {
        pUList[26].canBeApleed = false;
        pUList[27].canBeApleed = true;
        effStController.burnDieAura = true;
        GunFire.puFireSpeedBuff(15f);
        ExitFromPanel();
        Image iconImage = pUList[26].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "вкл огн ауру при смерти врага, +15% к скорострельности",
            "Enable fire aura on enemy death, +15% Fire Rate"
        ));
    }
    public void pU_6_3()
    {
        pUList[27].canBeApleed = false;
        pUList[28].canBeApleed = true;
        bulletPoulManager.puDamagePerchent(35f);
        bulletPoulManager.puSpeedPerchent(15f);
        ExitFromPanel();
        Image iconImage = pUList[27].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " 15% к скорострельности, +35% к урону от пуль",
            " 15% Fire Rate, +35% Bullet Damage"
        ));
    }
    public void pU_6_4()
    {
        pUList[28].canBeApleed = false;
        pUList[29].canBeApleed = true;
        effStController.burnDiraction += 2f;
        effStController.burnDamage += 3f;
        effStController.fireboll = true;
        ExitFromPanel();
        Image iconImage = pUList[28].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+2 сек горения, +3 урона горения, каждый 5 выстрел запускает фаербол",
            "+2 sec burn duration, +3 burn damage, every 5th shot launches a fireball"
        ));
    }
    public void pU_6_5()
    {
        pUList[29].canBeApleed = false;
        pUList[25].canBeApleed = true;
        effStController.burnDamage += 3f;
        effStController.fireAuraPlayer = true;
        ExitFromPanel();
        Image iconImage = pUList[29].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+3 урона горения, при полседнем выстреле запускает огненный вихрь",
            "+3 burn damage, last shot launches a fire vortex"
        ));
    }
    //Шрапнель
    // вкл ее 3 пули 60 градусов
    // +1 пуля +20 градусов
    // +20% урона
    // +1 пронзание
    // +20% урона
    public void pU_7_1()
    {
        pUList[30].canBeApleed = false;
        pUList[31].canBeApleed = true;
        if (!GunFire.activeEffects.Contains("Sharpnel"))
        {
            GunFire.activeEffects.Add("Sharpnel");
        }

        ExitFromPanel();
        Image iconImage = pUList[30].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "вкл шрапнель, 3 пули на вылет",
            "Enable shrapnel, 3 projectiles"
        ));
    }
    public void pU_7_2()
    {
        pUList[31].canBeApleed = false;
        pUList[32].canBeApleed = true;
        effStController.shrapnelCount += 1;
        effStController.spreadAngle += 20f;
        ExitFromPanel();
        Image iconImage = pUList[31].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+1 пуля шрпанели, +20 градусов",
            "+1 Shrapnel projectile, +20 degrees spread"
        ));
    }
    public void pU_7_3()
    {
        pUList[32].canBeApleed = false;
        pUList[33].canBeApleed = true;
        effStController.shrapnelBulletDamageInPecrent += 0.2f;
        ExitFromPanel();
        Image iconImage = pUList[32].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+20% урона от пуль шрапнели",
            "+20% Shrapnel damage"
        ));
    }
    public void pU_7_4()
    {
        pUList[33].canBeApleed = false;
        pUList[34].canBeApleed = true;
        effStController.shrapnelPenetration += 1;
        ExitFromPanel();
        Image iconImage = pUList[33].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " +1 пронзание для пуль шрапнели",
            " +1 Shrapnel penetration"
        ));
    }

    public void pU_7_5()
    {
        pUList[34].canBeApleed = false;
        pUList[30].canBeApleed = true;
        effStController.shrapnelBulletDamageInPecrent += 0.2f;
        ExitFromPanel();
        Image iconImage = pUList[34].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+20% урона от пуль шрапнели",
            "+20% Shrapnel damage"
        ));
    }
    
    // молния
    // 1	Шанс 10% спавна молнии при попадании
    // 2	Шанс 20%
    // 3	+ Урон молнии увеличен
    // 4	+ Радиус поражения молнии увеличен + ещё чуть-чуть урона
    // 5	Шанс 33% + Шанс 13% на спавн второй молнии
    public void pU_8_1()
    {
        pUList[35].canBeApleed = false;
        pUList[36].canBeApleed = true;
        if (!GunFire.activeEffects.Contains("LightingChain"))
        {
            GunFire.activeEffects.Add("LightingChain");
        }
        effStController.lightingDamage = 10f;
        effStController.lightningChance = 0.10f;
        effStController.lightingRadius = 3f;
        ExitFromPanel();
        Image iconImage = pUList[35].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "Шанс 10% спавна молнии при попадании",
            "10% Chance of lightning on hit"
        ));
    }
    public void pU_8_2()
    {
        pUList[36].canBeApleed = false;
        pUList[37].canBeApleed = true;
        effStController.lightningChance = 0.20f;
        ExitFromPanel();
        Image iconImage = pUList[36].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "Шанс 20% спавна молнии при попадании",
            "20% Chance of lightning on hit"
        ));
    }
    public void pU_8_3()
    {
        pUList[37].canBeApleed = false;
        pUList[38].canBeApleed = true;
        effStController.lightingDamage = 14f;
        ExitFromPanel();
        Image iconImage = pUList[37].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+ Урон молнии увеличен до 14 ед",
            "+ Lightning damage increased to 14"
        ));
    }
    public void pU_8_4()
    {
        pUList[38].canBeApleed = false;
        pUList[39].canBeApleed = true;
        effStController.lightingRadius = 3.5f;
        effStController.lightingDamage = 16f;
        ExitFromPanel();
        Image iconImage = pUList[38].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "+ Урон молнии увеличен до 16 ед, радиус до 3.5м",
            "+ Lightning damage increased to 16, radius to 3.5m"
        ));
    }
    public void pU_8_5()
    {
        pUList[39].canBeApleed = false;
        pUList[35].canBeApleed = true;
        effStController.lightningChance = 0.33f;
        effStController.allowSecondLightning = true;
        effStController.secondLightningChance = 0.13f;
        ExitFromPanel();
        Image iconImage = pUList[39].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "Шанс 33% + Шанс 13% на спавн второй молнии",
            "33% Chance + 13% Chance for second lightning"
        ));
    }
    //клинки
    // 1 клинок 10 урона
    // 2 клинка 12 урона
    // 3 клинка 14 урона
    // скорость 2x (240)
    // вкл штомр клинков 
    public void pU_9_1()
    {
        Image iconImage = pUList[40].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "1 клинок 10 урона",
            "1 Blade 10 Damage"
        ));
        pUList[40].canBeApleed = false;
        pUList[41].canBeApleed = true;
        ExitFromPanel();
        summonTowerEffector.BladeEnable();
        bladeEffect.InitializeBlades();
        bladeEffect.bladesCount += 1;
        if (bladeEffect.bladeDamage == 0) bladeEffect.bladeDamage = 10;
        else bladeEffect.bladeDamage += 10; // Чтобы на втором круге урон рос, а не падал
    }

    public void pU_9_2()
    {
        pUList[41].canBeApleed = false;
        pUList[42].canBeApleed = true;
        bladeEffect.bladesCount += 1;
        bladeEffect.bladeDamage = 12;
        bladeEffect.InitializeBlades();
        ExitFromPanel();
        Image iconImage = pUList[41].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " 2 клинка 12 урона",
            " 2 Blades 12 Damage"
        ));
    }
    public void pU_9_3()
    {
        pUList[42].canBeApleed = false;
        pUList[43].canBeApleed = true;
        bladeEffect.bladesCount += 1;
        bladeEffect.bladeDamage = 14;
        bladeEffect.InitializeBlades();
        ExitFromPanel();
        Image iconImage = pUList[42].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " 3 клинка 14 урона",
            " 3 Blades 14 Damage"
        ));
    }
    public void pU_9_4()
    {
        pUList[43].canBeApleed = false;
        pUList[44].canBeApleed = true;
        bladeEffect.rotationSpeed = 240f;
        bladeEffect.InitializeBlades();
        ExitFromPanel();
        Image iconImage = pUList[43].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "скорость вращения клинков 2x (240)",
            "Blade rotation speed 2x (240)"
        ));
    }
    public void pU_9_5()
    {
        pUList[44].canBeApleed = false;
        pUList[40].canBeApleed = true;
        bladeEffect.allowStrorm = true;
        ExitFromPanel();
        Image iconImage = pUList[44].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            " вкл шторм клинков ",
            " Enable Blade Storm "
        ));
    }
    //что то про яд, по сути чеснок)))
    //  добавляет игроку ауру слабенькую ауру яда                 (просто будем спавнить обькт под игроком(ненмого другую ауру огня) и привязывать его к игроку)
    // - чуть больше ее размер
    // - чуть большее ее урон
    // - добавляет ей немного замедления на врагов
    // - чуть больше размер, чяуть больше урон
    public void pU_10_1()
    {
        pUList[45].canBeApleed = false;
        pUList[46].canBeApleed = true;
        effStController.venomDamage += 3f;
        effStController.venomSizeMultiple = 1f;
        summonTowerEffector.VenomEnable();
        ExitFromPanel();
        Image iconImage = pUList[45].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "Спавнит под игроком небольшую ауру смертельного яда",
            "Spawns a small deadly poison aura under the player"
        ));
    }
    
    public void pU_10_2()
    {
        pUList[46].canBeApleed = false;
        pUList[47].canBeApleed = true;
        effStController.venomSizeMultiple = 1.2f;
        summonTowerEffector.VenomUpdate();
        ExitFromPanel();
        Image iconImage = pUList[46].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "увеличивает размер ауры яда на 20%",
            "Increases poison aura size by 20%"
        ));
    }
    
    public void pU_10_3()
    {
        pUList[47].canBeApleed = false;
        pUList[48].canBeApleed = true;
        effStController.venomDamage = 4f;
        summonTowerEffector.VenomUpdate();
        ExitFromPanel();
        Image iconImage = pUList[47].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "увеличивает урон ауры яда до 4 ед",
            "Increases poison aura damage to 4"
        ));
    }
    
    public void pU_10_4()
    {
        pUList[48].canBeApleed = false;
        pUList[49].canBeApleed = true;
        effStController.venomEnemySlownes = true;
        summonTowerEffector.VenomUpdate();
        ExitFromPanel();
        Image iconImage = pUList[48].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "замедляет всех врагов в зоне действия ауру в два раза",
            "Slows all enemies inside the aura by 50%"
        ));
    }
    
    public void pU_10_5()
    {
        pUList[49].canBeApleed = false;
        pUList[45].canBeApleed = true;
        effStController.venomDamage = 5f;
        effStController.venomSizeMultiple = 1.4f;
        summonTowerEffector.VenomUpdate();
        ExitFromPanel();
        Image iconImage = pUList[49].GetComponent<Image>();
        AddUsedPU(iconImage.sprite, GetLoc(
            "увеличивает ауру в два раза, увеличивает ее урон до 5 ед",
            "Doubles aura size, increases damage to 5"
        ));
    }
}
