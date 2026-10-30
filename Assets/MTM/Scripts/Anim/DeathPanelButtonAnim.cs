using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathPanelButtonAnim : MonoBehaviour
{
    public GameObject buttonReturn;
    public GameObject buttonMenu;
    public GameObject buttonAD;
    public GameObject sniperCircle;
    public StatsSaved st;
    public GameStats gt;
    public Animator anim;

    public bool gameEnded = false;
    void Start()
    { 
        st = FindObjectOfType<StatsSaved>();
        gt = FindObjectOfType<GameStats>();
        sniperCircle.SetActive(false);
        anim = GetComponent<Animator>();
        PlayerIsDeathMarkerForAnim();
    }
    
    public void PlayerIsDeathMarkerForAnim()
    {
        anim.SetBool("PlayerIsDeath",true);
    }

    public void UnActiveButtons()
    {
        buttonReturn.SetActive(false);
        buttonMenu.SetActive(false);
        buttonAD.SetActive(false);
    }

    public void ButtonSpawnFirst()
    {
        buttonReturn.SetActive(true); 
    }

    public void ButtonSpawnSecond()
    {
        buttonMenu.SetActive(true);
    }
    
    public void ADButtonSpawn()
    {
        if (!gameEnded)
        {
            buttonAD.SetActive(true);
        }
    }
}
