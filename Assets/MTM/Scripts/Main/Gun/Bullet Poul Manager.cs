using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoulManager : MonoBehaviour
{
    
    public static BulletPoulManager SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject bulletPrefab;
    public Bullets counteiner;
    public int amountToPool;
    public GameObject statsUp;

    public float globalDamageBuff = 0f;
    public float globalSpeedBuff = 0f;
    public float globalScaleBuff = 0f;
    public int globalPenetrationBuff = 0;


    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(bulletPrefab);
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
                
                // Сброс баффа (как было)
                Bullet statsUp = bulletObj.GetComponent<Bullet>();
                statsUp.bigbaff = 0;
                
                // 💥 Установка эффектов
                BulletEffectController controller = bulletObj.GetComponent<BulletEffectController>();
                if (controller != null && effectIDs != null)
                {
                    controller.SetActiveEffectsByIDs(effectIDs);
                    Debug.Log("установил айди такой вот " + effectIDs);
                }

                return bulletObj;
            }
        }
        return null;
    }
    
    public GameObject GetBigPooledObject(List<string> effectIDs = null)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                GameObject bulletObj = pooledObjects[i];

                Bullet statsUp = bulletObj.GetComponent<Bullet>();
                statsUp.bigbaff = 2;

                // 💥 Установка эффектов
                BulletEffectController controller = bulletObj.GetComponent<BulletEffectController>();
                if (controller != null && effectIDs != null)
                {
                    controller.SetActiveEffectsByIDs(effectIDs);
                }

                return bulletObj;
            }
        }
        return null;
    }

    public void puDamagePerchent(float damageBuff) // от 1 до 100 %
    {        
        globalDamageBuff *= 1 + (damageBuff / 100f);
    }  

    public void puDamage(float damageBuff = 1f)
    {
        globalDamageBuff += damageBuff;
    }   

    public void puScale(float scaleBuff = 0.2f)
    {
        globalScaleBuff += scaleBuff;
    }

    public void puSpeed(float speedBuff = 2.5f)
    {
        globalSpeedBuff += speedBuff;
    }

    public void puSpeedPerchent(float percent) // от 1 до 100 %
    {        
        globalSpeedBuff *= 1 + (percent / 100f);
    }  

    public void puPenetrationBuff(int i)
    {
        globalPenetrationBuff += i;
    }
}
