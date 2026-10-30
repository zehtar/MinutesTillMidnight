using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHeal : MonoBehaviour
{
    public GameObject healObjectPrefab;
    public Spawner spawner;
    public bool colision;

    public bool healCanBeSpawned = false;
    
    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    public void Update()
    {
        if (spawner.enemyAllKilled % 3 == 0 && healCanBeSpawned)
        {
            //Instantiate(healObjectPrefab, transform.position, Quaternion.identity);
            healCanBeSpawned = false;
        }
    }
    public void HealSummonCheck()
    {
        if (spawner.enemyAllKilled % 3 == 0)
        {
            healCanBeSpawned = true;
        }
    }
}
