using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LamiActivator : MonoBehaviour
{
    public GameObject lamiPowerPrefab;
    public Image PowerBlackImage;
    public Text CouldownText;
    public player player;

    public float cooldownTimer;
    public float couldown;
    public bool canActive;
        
    public void Update()
    {
        PowerBlackImage.fillAmount = 1 - (cooldownTimer / couldown);
        CouldownText.text = Math.Round((couldown - cooldownTimer),1).ToString();
      
        if (cooldownTimer < couldown)
        {
            cooldownTimer += Time.deltaTime;
            canActive = false;
            PowerBlackImage.gameObject.SetActive(true);
            CouldownText.gameObject.SetActive(true);
        }
        else if (cooldownTimer >= couldown)
        {
            canActive = true;
            PowerBlackImage.gameObject.SetActive(false);
            CouldownText.gameObject.SetActive(false);
        }
        
        if (Input.GetMouseButton(1))
        {
            ActivePower();
        }
    }

    public void ActivePower()
    {
        if (canActive)
        {
            Instantiate(lamiPowerPrefab, player.gameObject.transform.position, Quaternion.identity);
            cooldownTimer = 0f;
            canActive = false;
        }
    }
}
