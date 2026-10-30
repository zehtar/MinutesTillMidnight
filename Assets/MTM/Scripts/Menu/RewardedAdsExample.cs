using UnityEngine;
using YG; // Это правильное пространство имён для официального SDK Яндекс.Игр

public class RewardedAdsExample : MonoBehaviour
{
    public int coins;
    public int gems;

    public StatsSaved statsSaved;
    public SaveStatsInMenu saveStatsInMenu;
    public Level level;
    public player player;
    public AudioManager audioManager;
    private void Start()
    {
        statsSaved = FindObjectOfType<StatsSaved>();
        saveStatsInMenu = FindObjectOfType<SaveStatsInMenu>();
        level = FindObjectOfType<Level>();
        player = FindObjectOfType<player>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnEnable()
    {
        YG2.onRewardAdv += OnReward;
    }

    private void OnDisable()
    {
        YG2.onRewardAdv -= OnReward;
    }

    public void ShowRewardAd(string id)
    {
        if (player == null)
        {
            player = FindObjectOfType<player>();
        }
        if (level == null)
        {
            level = FindObjectOfType<Level>();
        }
        
        Debug.Log("Показываем рекламу с ID: " + id);
        audioManager.loadMusic = false;
        YG2.RewardedAdvShow(id);
    }

    private void OnReward(string id)
    {
        Debug.Log("Получена награда с ID: " + id);
        audioManager.loadMusic = true;
        if (id == "coinRewardID")
        {
            statsSaved.coins += 1000;
            coins += 1000;
            saveStatsInMenu.SaveAllData();
            Debug.Log("Выданы монеты!");
        }
        else if (id == "expRewardID")
        {
            level.adRevardedExpBuff = true;
            Debug.Log("Выдан x2 опыт!");
        }
        else if (id == "reviveRewardID")
        {
            player.PlayerRevive();
            Debug.Log("воскрешен!");
        }
    }
}