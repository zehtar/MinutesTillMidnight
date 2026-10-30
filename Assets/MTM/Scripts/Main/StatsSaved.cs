using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType{PC , Android};

public class StatsSaved : MonoBehaviour
{
    public static StatsSaved Instance;

    public int indexScene;
    public UImanager ui;
    //player
    public string characterName;
    public float playerspeed = 5;   //1
    public float playerhealth = 3;  //2
    public int playerlevel = 0;     //0
    public float regenerationRate;
    //player Active Powers
    public float powersDamage;
    public float powersSize;
    public float powersDuration;
    //exp
    public float collectRange = 1f;  //3
    public float expMultiply = 1f;  //4
    //gun
    public bool isSuperWeapon = false;
    public string gunSelected;
	public float gunfireRate = 1; // скорострельность //7
	public int gunshotPerShot = 1;  //9
    public int gunlevel = 0;        //5
    public int passivesLevel;
    public int penetration;
    public int bulletsInMagazine = 3;
    public float spread = 10f;
    public float reloadTime = 1f;
    public float critChance = 0f;
    public float critDamage = 0.5f;
    //bullet
    public float bulletspeed = 10;  //8
	public float bulletdamage = 1;  //6
    public int bulletrotateDown = 5;    
	public int bulletrotateUp = 5;
    //enemy
    public float enemySpeed;
    public int enemyLevel;
    public float eneeyBossHpMyltiply;
    public int enemyDamage;
    //game
    public float CoinMyltiply;
    public int coins;

    //public bool[] buyChecked;
    public ControlType controlType;

    public WeaponStats selectedWeapon = new WeaponStats();
    
    //скопированно с unity дока
    void Awake()
    {
        ui = FindObjectOfType<UImanager>();
        indexScene = ui.indexScene;
        if (Instance == null) 
        {
            Instance = this; // Сохраняем ссылку на этот объект
        }
        
        GameObject[] objs = GameObject.FindGameObjectsWithTag("StatSave");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
}
