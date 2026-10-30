using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public ExpCoin expCoin;
    //public GameObject expCircle;
    //public Enemy enemy;
    public Spawner spawner;
    //public Level lv;
    public int expNumber;
    private void Start()
    {
       // lv = FindObjectOfType<Level>();
        spawner = FindObjectOfType<Spawner>();
    }
    
    private void Update()
    {
       // expNumber = Mathf.RoundToInt(spawner.enemyExp);
    }
}
