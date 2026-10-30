using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelBullletPoulManager : MonoBehaviour
{
    public static ShrapnelBullletPoulManager SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject shrapnelBulletPrefab;
    public GameObject counteiner;
    public int amountToPool;
    
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject tmp;
            tmp = Instantiate(shrapnelBulletPrefab);
            tmp.transform.SetParent(counteiner.gameObject.transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    
    public GameObject GetPooledObject(List<string> effectIDs = null)
    {
        for (int i = Random.Range(0,amountToPool); i < amountToPool; i = Random.Range(0,amountToPool))
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                GameObject bulletObj = pooledObjects[i];
                return bulletObj;
            }
        }
        return null;
    }
}
