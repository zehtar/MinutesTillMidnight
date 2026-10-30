using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public string characterName;
    public float speed;
    public float health;
    public int startLevel;
    public int passiveslevel;
    public float collectRange;
    public float regenerationRate;
    //powers
    public float powerCD;
    public float powersDamage;
    public float powersSize;
    public float powersDuration;
}

