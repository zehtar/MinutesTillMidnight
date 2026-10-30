using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomEffect : MonoBehaviour
{
    public GameObject venomAuraPrefab;
    public EffectsStatsController  statsController;
    public player player;
    public GameObject aura;
    public void Start()
    {
        statsController = FindObjectOfType<EffectsStatsController>();
        player = FindObjectOfType<player>();
    }
    public void SummonVenomAura()
    {
        if (player == null)
        {
            player = FindObjectOfType<player>();
            Debug.Log("нашел игрока для венома");
        }

        aura = Instantiate(venomAuraPrefab, player.transform.position, Quaternion.identity);
        aura.transform.SetParent(player.gameObject.transform);
        UpdateVenomAura();
    }

    public void UpdateVenomAura()
    {
        if (statsController == null)
        {
            statsController = FindObjectOfType<EffectsStatsController>();
            Debug.Log("нашел EffectsStatsController для венома");
        }
        aura.GetComponent<FireArea>().duration = 99999999;
        aura.GetComponent<FireArea>().damagePerTick = statsController.venomDamage;
        aura.GetComponent<FireArea>().enemySlownes = statsController.venomEnemySlownes;
        aura.transform.localScale *= statsController.venomSizeMultiple;
    }
}
