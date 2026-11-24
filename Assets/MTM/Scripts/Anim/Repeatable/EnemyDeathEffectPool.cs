using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffectPool : MonoBehaviour
{
    public static EnemyDeathEffectPool Instance;

    [Header("Pool Settings")]
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private int initialPoolSize = 10;

    private readonly List<EnemyDeathEffect> pooledEffects = new List<EnemyDeathEffect>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateEffect();
        }
    }

    private EnemyDeathEffect CreateEffect()
    {
        GameObject obj = Instantiate(effectPrefab, container);
        obj.SetActive(false);

        EnemyDeathEffect effect = obj.GetComponent<EnemyDeathEffect>();

        effect.SetPool(this);
        pooledEffects.Add(effect);

        return effect;
    }

    public void PlayEffect(Vector3 position, Quaternion rotation)
    {
        EnemyDeathEffect effect = null;

        for (int i = 0; i < pooledEffects.Count; i++)
        {
            if (!pooledEffects[i].gameObject.activeSelf)
            { 
                effect = pooledEffects[i];
                break;
            }
        }

        if (effect == null)
        {
            effect = CreateEffect();
        }

        if (effect != null)
        {
            effect.Play(position, rotation);
        }
    }

    public void ReturnToPool(EnemyDeathEffect effect)
    {
        effect.gameObject.SetActive(false);
    }
}