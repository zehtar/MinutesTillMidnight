using UnityEngine;

public class BulletHitEffect : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    private BulletHitEffectPool pool;

    private bool isPlaying;

    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>(true);
    }

    public void SetPool(BulletHitEffectPool effectPool)
    {
        pool = effectPool;
    }

    public void Play(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);

        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        gameObject.SetActive(true);

        isPlaying = true;

        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play(false);
        }
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        foreach (ParticleSystem ps in particleSystems)
        {
            if (ps.IsAlive(false))
                return;
        }

        isPlaying = false;
        pool.ReturnToPool(this);
    }
}