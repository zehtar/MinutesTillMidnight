using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChankController : MonoBehaviour
{
    public GameObject[] chunkPrefabs; // Массив префабов чанков
    public float chunkSize = 27f;  // Размер чанка (должен совпадать с фактическим размером чанка)
    private HashSet<Vector2> spawnedChunks = new HashSet<Vector2>(); // Хранит координаты уже созданных чанков
    public player player;

    public bool isCaveLevel = false;

    private void Start()
    {
        player = FindObjectOfType<player>(); 
        this.transform.SetParent(player.gameObject.transform);
        // Спавним начальный чанк в начале игры
        /*
        Vector2 initialChunkPosition = new Vector2(
            Mathf.Round(transform.position.x / chunkSize) * chunkSize,
            Mathf.Round(transform.position.y / chunkSize) * chunkSize
        );
        SpawnChunkAt(initialChunkPosition);
        */
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Определяем текущую позицию игрока в координатах чанка
        Vector2 playerChunkPosition = new Vector2(
            Mathf.Round(transform.position.x / chunkSize) * chunkSize,
            Mathf.Round(transform.position.y / chunkSize) * chunkSize
        );

        // Спавним чанки вокруг текущего положения игрока (включая диагонали)
        for (int x = -1; x <= 1; x++)
        {
            if (!isCaveLevel)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector2 offset = new Vector2(x * chunkSize, y * chunkSize);
                    SpawnChunkAt(playerChunkPosition + offset);
                }
            }
            else
            {
                Vector2 offset = new Vector2(x * chunkSize,7);
                SpawnChunkAt(playerChunkPosition + offset);
            }
        }
    }

    private void SpawnChunkAt(Vector2 position)
    {
        // Проверяем, если чанк в этой позиции уже создан
        if (!spawnedChunks.Contains(position))
        {
            GameObject randomChunk = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)]; // Выбираем случайный чанк
            Instantiate(randomChunk, position, Quaternion.identity);  // Создаем чанк
            spawnedChunks.Add(position); // Добавляем координаты нового чанка в список
        }
    }
}




