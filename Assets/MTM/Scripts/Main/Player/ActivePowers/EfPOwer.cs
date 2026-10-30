using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EfPOwer : MonoBehaviour
{
    public Image PowerBlackImage;
    public Text CouldownText;
    public player player;
    public FireScript gun;

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
            StartCoroutine(TemporaryBoost(0.5f, 7f));
            cooldownTimer = 0f;
            canActive = false;
        }
    }
    
    private IEnumerator TemporaryBoost(float multiplier, float duration)
    {
        if (player == null || gun == null)
        {
            player = FindAnyObjectByType<player>();
            gun = FindAnyObjectByType<FireScript>();
        }

        float originalSpeed = player.speed;
        float originalSlowSpeed = player.slowSpeed;
        float originalNormalSpeed = player.normalSpeed;
        player.speed /= multiplier;
        player.slowSpeed /= multiplier;
        player.normalSpeed /= multiplier;

        float originalGunFireRate = gun.baseCurTimeOut;
        gun.baseCurTimeOut *= multiplier;
        gun.curTimeout *= multiplier;

        yield return new WaitForSeconds(duration);

        player.speed = originalSpeed;
        player.slowSpeed = originalSlowSpeed;
        player.normalSpeed = originalNormalSpeed;
        
        gun.baseCurTimeOut = originalGunFireRate;
    }
}
