using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeEffect : MonoBehaviour
{
    public GameObject bladePrefab;
    public Transform player;
    
    public List<Blade> blades = new List<Blade>();
    
    public float baseRadius = 1.5f;
    public float radiusMultiplier = 1.0f;
    public float rotationSpeed = 120f;
    public int bladeDamage = 20;
    public int bladesCount;

    public bool isReloading = false;
    public bool allowStrorm;

    void Start()
    {
        player = FindObjectOfType<player>().transform;
        InitializeBlades();
    }

    public void InitializeBlades()
    {
        // Убираем старые клинки
        foreach (var blade in blades)
        {
            if (blade != null) Destroy(blade.gameObject);
        }
        blades.Clear();
        
        // Спавним клинки
        for (int i = 0; i < bladesCount; i++)
        {
            GameObject bladeObj = Instantiate(bladePrefab, player.position, Quaternion.identity);
            Blade blade = bladeObj.GetComponent<Blade>();
            player = FindObjectOfType<player>().transform;
            blade.player = player;
            blade.radius = baseRadius * radiusMultiplier;
            blade.rotationSpeed = rotationSpeed;
            blade.damage = bladeDamage;

            blades.Add(blade);
        }

        UpdateBladesPosition();
    }

    void UpdateBladesPosition()
    {
        for (int i = 0; i < blades.Count; i++)
        {
            blades[i].UpdateBladePosition(i, blades.Count);
        }
    }

    // Этот метод должен вызываться из системы оружия, когда начинается перезарядка
    public void OnReloadStart()
    {
        if (!allowStrorm) return; // Шторм доступен только на 5 уровне

        SpawnBladeStorm();
    }

    void SpawnBladeStorm()
    {
        int stormBladeCount = 10; // Количество клинков в шторме
        float angleStep = 360f / stormBladeCount; // Угол между каждым клинком

        for (int i = 0; i < stormBladeCount; i++)
        {
            // Инстанцируем клинок
            GameObject bladeObj = Instantiate(bladePrefab, player.position, Quaternion.identity);
            Blade blade = bladeObj.GetComponent<Blade>();
            blade.player = null; // Не крутимся вокруг игрока
            blade.isStormBlade = true;

            // Вычисление угла для текущего клинка, чтобы они располагались по кругу
            float angle = i * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right; // Направление клинка

            // Устанавливаем направление штормового клинка
            blade.SetStormDirection(dir.normalized);

            // Устанавливаем урон
            blade.damage = (int)(bladeDamage);

            // Удаляем клинок через 2 секунды
            Destroy(bladeObj, 2f); 
        }
    }

}

