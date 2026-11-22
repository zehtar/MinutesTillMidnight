using UnityEngine;

public class MuzzleFlashEffect : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    private MuzzleFlashPool pool;

    private bool isPlaying;

    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>(true);
    }

    public void SetPool(MuzzleFlashPool effectPool)
    {
        pool = effectPool;
    }

    public void Play(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation * Quaternion.Euler(0f,0f,90f));

        gameObject.SetActive(true);

        // Сбрасываем все системы перед повторным запуском.
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        isPlaying = true;

        // Запускаем все три эффекта одновременно.
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play(false);
        }
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        // Проверяем, завершились ли все Particle System.
        foreach (ParticleSystem ps in particleSystems)
        {
            if (ps.IsAlive(false))
                return;
        }

        isPlaying = false;
        pool.ReturnToPool(this);
    }
}