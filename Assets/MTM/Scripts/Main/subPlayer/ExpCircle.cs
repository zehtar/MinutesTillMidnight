using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCircle : MonoBehaviour
{
    public Level _level; // Ссылка на lvlManager
    public player player;
    public PowerUpsPoulManager bossDropPanel;

    private void Start()
    {
        _level = FindObjectOfType<Level>();
        player = FindObjectOfType<player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExpCoin"))
        {
            ExpCoin expC = other.GetComponent<ExpCoin>();
            _level.AddExperience(expC.expNum);
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("HealCoin"))
        {
            HealCoin expC = other.GetComponent<HealCoin>();
            if (player.health < player.numOfHearts)
            {
                player.HeartAdd();
            }
            AudioManager.Instance.PlayPowerUp();
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("bossDrop"))
        {
            bossDropPanel.bossDrop = true;
            _level.AddExperience(50);
            Destroy(other.gameObject);
            AudioManager.Instance.PlayPowerUp();
        }
    }
}


