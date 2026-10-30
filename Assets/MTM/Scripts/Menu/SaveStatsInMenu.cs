using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveStatsInMenu : MonoBehaviour
{
    public StatsSaved st;
    public bool[] buyGunChecked = new bool[7];
    public bool[] buyPlChecked = new bool[5];

    public bool[] upgradeCheckedGun1 = new bool[5];
    public bool[] upgradeCheckedGun2 = new bool[5];
    public bool[] upgradeCheckedGun3 = new bool[5];
    public bool[] upgradeCheckedGun4 = new bool[5];
    public bool[] upgradeCheckedGun5 = new bool[5];
    public bool[] upgradeCheckedGun6 = new bool[5];
    
    public bool[] upgradeCheckePlayer1 = new bool[5];
    public bool[] upgradeCheckePlayer2 = new bool[5];
    public bool[] upgradeCheckePlayer3 = new bool[5];
    
    public bool[] magazineCheckedGun = new bool[7];
    public bool[] magazineCheckedPlayer = new bool[5];
    
    public bool[] magazineSkins = new bool[7];
    
    public bool storyEnd = false;
    
    public int[] shopItemLevels = new int[10];
    
    public bool levelFirstComplite = false;
    public bool levelSecondComplite = false;
    public bool levelThirdComplite = false;
    
    void Awake()
    {
        st = FindObjectOfType<StatsSaved>();
        LoadAllData();
    }

    public void SaveDiffDate()
    {
        SaveBool("levelFirstComplite", levelFirstComplite);
        SaveBool("levelSecondComplite", levelSecondComplite);
        SaveBool("levelThirdComplite", levelThirdComplite);
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", st.coins);
    }

    public void SaveStory(bool storyEnd)
    {
        SaveBool("storyEnd", storyEnd);
    }

    public void SaveAllData()
    {
        PlayerPrefs.SetInt("Coins", st.coins);
       
        SaveBoolArray("buyGunChecked", buyGunChecked);
        SaveBoolArray("buyPlChecked", buyPlChecked);
        
        SaveBoolArray("upgradeCheckedGun1", upgradeCheckedGun1);
        SaveBoolArray("upgradeCheckedGun2", upgradeCheckedGun2);
        SaveBoolArray("upgradeCheckedGun3", upgradeCheckedGun3);
        SaveBoolArray("upgradeCheckedGun4", upgradeCheckedGun4);
        SaveBoolArray("upgradeCheckedGun5", upgradeCheckedGun5);
        SaveBoolArray("upgradeCheckedGun6", upgradeCheckedGun6);
        
        SaveBoolArray("upgradeCheckePlayer1", upgradeCheckePlayer1);
        SaveBoolArray("upgradeCheckePlayer2", upgradeCheckePlayer2);
        SaveBoolArray("upgradeCheckePlayer3", upgradeCheckePlayer3);
        
        SaveBoolArray("magazineCheckedGun",magazineCheckedGun);
        SaveBoolArray("magazineCheckedPlayer",magazineCheckedPlayer);
        SaveBoolArray("magazineSkins",magazineSkins);
        
        for (int i = 0; i < shopItemLevels.Length; i++)
        {
            PlayerPrefs.SetInt("ShopItemLevel_" + i, shopItemLevels[i]);
        }
      
        PlayerPrefs.Save(); 
    }

    public void LoadAllData()
    {
        st.coins = PlayerPrefs.GetInt("Coins", 0);

        buyGunChecked = LoadBoolArray("buyGunChecked", 7);
        buyPlChecked = LoadBoolArray("buyPlChecked", 5);
        
        upgradeCheckedGun1 = LoadBoolArray("upgradeCheckedGun1", 5);
        upgradeCheckedGun2 = LoadBoolArray("upgradeCheckedGun2", 5);
        upgradeCheckedGun3 = LoadBoolArray("upgradeCheckedGun3", 5);
        upgradeCheckedGun4 = LoadBoolArray("upgradeCheckedGun4", 5);
        upgradeCheckedGun5 = LoadBoolArray("upgradeCheckedGun5", 5);
        upgradeCheckedGun6 = LoadBoolArray("upgradeCheckedGun6", 5);
        
        upgradeCheckePlayer1 = LoadBoolArray("upgradeCheckePlayer1", 5);
        upgradeCheckePlayer2 = LoadBoolArray("upgradeCheckePlayer2", 5);
        upgradeCheckePlayer3 = LoadBoolArray("upgradeCheckePlayer3", 5);

        magazineCheckedGun = LoadBoolArray("magazineCheckedGun", 7);
        magazineCheckedPlayer = LoadBoolArray("magazineCheckedPlayer", 5);
        magazineSkins = LoadBoolArray("magazineSkins", 7);
        
        storyEnd = LoadBool("storyEnd");

        levelFirstComplite = LoadBool("levelFirstComplite");
        levelSecondComplite = LoadBool("levelSecondComplite");
        levelThirdComplite = LoadBool("levelThirdComplite");
        
        for (int i = 0; i < shopItemLevels.Length; i++)
        {
            shopItemLevels[i] = PlayerPrefs.GetInt("ShopItemLevel_" + i, 0); // по умолчанию 0
        }
    }

    public void ResetAllData()
    {
        PlayerPrefs.SetInt("Coins", 0);
        
        SaveBool("diffComplite1", false);
        SaveBool("diffComplite2",false);
        
        bool[] emptyArray = new bool[7]; 
        
        SaveBoolArray("buyGunChecked", emptyArray);
        SaveBoolArray("buyPlChecked", emptyArray);
        
        SaveBoolArray("upgradeCheckedGun1", emptyArray);
        SaveBoolArray("upgradeCheckedGun2", emptyArray);
        SaveBoolArray("upgradeCheckedGun3", emptyArray);
        SaveBoolArray("upgradeCheckedGun4", emptyArray);
        SaveBoolArray("upgradeCheckedGun5", emptyArray);
        SaveBoolArray("upgradeCheckedGun6", emptyArray);
        
        SaveBoolArray("upgradeCheckePlayer1", emptyArray);
        SaveBoolArray("upgradeCheckePlayer2", emptyArray);
        SaveBoolArray("upgradeCheckePlayer3", emptyArray);
        
        SaveBoolArray("magazineCheckedGun", emptyArray);
        SaveBoolArray("magazineCheckedPlayer", emptyArray);
        SaveBoolArray("magazineSkins",emptyArray);
        
        SaveBool("storyEnd", false);
        
        SaveBool("levelFirstComplite", false);
        SaveBool("levelSecondComplite", false);
        SaveBool("levelThirdComplite", false);
        
        for (int i = 0; i < shopItemLevels.Length; i++)
        {
            shopItemLevels[i] = 0;
            PlayerPrefs.SetInt("ShopItemLevel_" + i, 0);
        }
        
        PlayerPrefs.Save(); 
    }
    
    private void SaveBoolArray(string key, bool[] array)
    {
        string value = string.Join("", array.Select(b => b ? "1" : "0"));
        PlayerPrefs.SetString(key, value);
    }

    private bool[] LoadBoolArray(string key, int size)
    {
        string value = PlayerPrefs.GetString(key, new string('0', size)); 
        return value.Select(c => c == '1').ToArray();
    }

    private void SaveBool(string key, bool yesOrNot)
    {
        int num;
        if (yesOrNot)
        {
            num = 1;
        }
        else
        {
            num = 0;
        }
        PlayerPrefs.SetInt(key, num);
    }

    private bool LoadBool(string key)
    {
        int num = PlayerPrefs.GetInt(key, 0);
        if (num == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
