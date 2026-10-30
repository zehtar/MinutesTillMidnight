using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class PowerUpsPoulManager : MonoBehaviour
{
    public PowerUpIdent[] powerUps;

    private int _oldRandom;
    private int _objectOnSceneNow;
    public PowerUpsScript puScript;
    public GameObject healPowerUp;

    public GameObject[] confirmList;

    public bool bossDrop = false;
    public void Start()
    {
        puScript = FindObjectOfType<PowerUpsScript>(); 
    }

    public void OnEnable()
    {
        if (bossDrop)
        {
            RandoomBossSummon();
            bossDrop = false;
        }
        else
        {
            RandoomSummon();
        }
        
        FireEnableDisable fireStop = FindObjectOfType<FireEnableDisable>();
        fireStop.DisableFire();
    }

    public void RandoomSummon()
    {
        
        foreach (var pu in powerUps)
        {
            pu.gameObject.SetActive(false);
        }
        healPowerUp.SetActive(false);

        // Фильтруем доступные объекты
        List<PowerUpIdent> availablePowerUps = powerUps.Where(p => p.canBeApleed && !p.isBossDrop).ToList();

        _objectOnSceneNow = 0; // Сбрасываем счётчик

        // Выбираем до 4 случайных объектов
        int maxSpawn = Mathf.Min(4, availablePowerUps.Count);
        if (maxSpawn != 0)
        {
            for (int i = 0; i < maxSpawn; i++)
            {
                int index = Random.Range(0, availablePowerUps.Count);
                availablePowerUps[index].gameObject.SetActive(true);
                availablePowerUps.RemoveAt(index); // Удаляем, чтобы не выбрать повторно
                _objectOnSceneNow++;
            }
        }
        else
        {
            healPowerUp.SetActive(true);
        }
    }
    
    public void RandoomBossSummon()
    {
        
        foreach (var pu in powerUps)
        {
            pu.gameObject.SetActive(false);
        }

        // Фильтруем доступные объекты
        List<PowerUpIdent> availablePowerUps = powerUps.Where(p => p.canBeApleed && p.isBossDrop).ToList();

        _objectOnSceneNow = 0; // Сбрасываем счётчик

        // Выбираем до 3 случайных объектов
        int maxSpawn = Mathf.Min(3, availablePowerUps.Count);
        for (int i = 0; i < maxSpawn; i++)
        {
            int index = Random.Range(0, availablePowerUps.Count);
            availablePowerUps[index].gameObject.SetActive(true);
            availablePowerUps.RemoveAt(index); // Удаляем, чтобы не выбрать повторно
            _objectOnSceneNow++;
        }
    }

    public void ConfirmCleaner()
    {
        for(int i = 0; i < confirmList.Length;i++)
        {
            confirmList[i].gameObject.SetActive(false);
            
            FireEnableDisable fireStop = FindObjectOfType<FireEnableDisable>();
            fireStop.EnableFire();
        }
    }
}