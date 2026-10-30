using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int level = 1;
    public float levelExpNeed = 10f;
    public float exp;
    public LevelUpAnim LevelUpAnimPrefab;
    public GameObject powerUpPamel;
    public GameObject expGainEffect;
    public player pl;
    public Image experienceImage;

    public GameObject knockBackPrefab;

    public bool adRevardedExpBuff = false;
    public void Start()
    {   
        pl = FindObjectOfType<player>();
    }

    void Update()
    {
        if (pl.health > 0)
        {
            if (exp >= levelExpNeed)
            {
                exp -= levelExpNeed;
                LevelUpAnim lvlAnim = Instantiate(LevelUpAnimPrefab, transform.position, transform.rotation);
                GameObject player = FindObjectOfType<player>().gameObject;
                lvlAnim.transform.SetParent(player.transform);
            }
            experienceImage.fillAmount = exp / levelExpNeed;
        }
    }

    public void LevelUp()
    {
        level++;
        Instantiate(knockBackPrefab, transform.position, Quaternion.identity);
        levelExpNeed *= 1.4f;
        powerUpPamel.SetActive(true);
        Time.timeScale = 0;
    }

    public void AddExperience(int expGet)
    {
        Instantiate(expGainEffect , experienceImage.gameObject.transform.position , Quaternion.identity);
        if (adRevardedExpBuff)
        {
            exp += expGet * 2;
        }
        else
        {
            exp += expGet;
        }
    } 
}
