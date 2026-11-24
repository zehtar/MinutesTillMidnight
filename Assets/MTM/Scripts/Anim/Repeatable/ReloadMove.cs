using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadMove : MonoBehaviour
{
    public FireScript gun;
    public player player;
    public Image reloadLine;
    public BladeEffect bladeEffect;
    public EffectsStatsController effectsStatsController;
    public float timer;
    public float endOfTimer;
    public GameObject playerCanvas;

    public StatsSaved statsSaved;
    public bool localFlipRight;
    void Start()
    {
        gun = FindObjectOfType<FireScript>();
        bladeEffect = FindObjectOfType<BladeEffect>();
        effectsStatsController = FindAnyObjectByType<EffectsStatsController>();
        player = FindObjectOfType<player>();
        statsSaved = FindObjectOfType<StatsSaved>();
        endOfTimer = statsSaved.reloadTime;
        
        playerCanvas = player.GetComponentInChildren<Canvas>().gameObject;
        transform.SetParent(playerCanvas.transform);
        transform.localPosition = new Vector3(0, -2, 0);
    }
    
    private void Flip()
    {
        localFlipRight = !localFlipRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void ReloadOver()
    {
        gun.bulletHave = gun.bulletMax;
        gun.isReloading = false;    
        gameObject.SetActive(false);
    }

    public void ReloadStart()
    {
        if (bladeEffect != null)
        {
            bladeEffect.OnReloadStart();
        }
        else
        {
            bladeEffect = FindObjectOfType<BladeEffect>();
        }

        if (effectsStatsController == null)
        {
            effectsStatsController = FindAnyObjectByType<EffectsStatsController>();
        }

        if (effectsStatsController.fireAuraPlayer)
        {
            GameObject fireAura = Instantiate(effectsStatsController.fireAuraPlayerPrefab, gun.gameObject.transform.position, Quaternion.identity);
            fireAura.transform.localScale = new Vector3(5, 5, 1);
            fireAura.GetComponent<FireArea>().damagePerTick = 15;
            fireAura.GetComponent<FireArea>().duration = 0.4f;
        }
    }

    public void Update()
    {
        if(timer < endOfTimer)
        {
            timer += Time.deltaTime;
        }
        else 
        {
            timer = 0.1f;
            ReloadOver();
        }

        reloadLine.fillAmount = timer / endOfTimer;

        if (player.flipRight != localFlipRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        //transform.position = player.GetComponent<Transform>().position - offset;
    }

    public void puCouldownBuff(float percent)
    {
        endOfTimer *= 1 - (percent/100);
    }
    
    
}
