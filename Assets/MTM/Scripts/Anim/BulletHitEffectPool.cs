using System.Collections.Generic;
using UnityEngine;

public class BulletHitEffectPool : MonoBehaviour
{
    public static BulletHitEffectPool Instance;

    [Header("Pool Settings")]
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private int initialPoolSize = 10;

    private readonly List<BulletHitEffect> pooledEffects =
        new List<BulletHitEffect>();

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

    private BulletHitEffect CreateEffect()
    {
        GameObject obj = Instantiate(effectPrefab, container);
        obj.SetActive(false);

        BulletHitEffect effect =
            obj.GetComponent<BulletHitEffect>();

        effect.SetPool(this);
        pooledEffects.Add(effect);

        return effect;
    }

    public void PlayEffect(Vector3 position, Quaternion rotation)
    {
        BulletHitEffect effect = null;

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

        effect.Play(position, rotation);
    }

    public void ReturnToPool(BulletHitEffect effect)
    {
        effect.gameObject.SetActive(false);
    }
}