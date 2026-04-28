using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 0.5f;

    private int maxEnemies;
    private int totalSpawned;

    void Awake()
    {
        instance = this;
    }


    public void SpawnWave(int amount)
    {
        maxEnemies = amount;
        totalSpawned = 0;

        CancelInvoke(nameof(SpawnEnemy));
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    public void StopSpawning()
    {
        CancelInvoke(nameof(SpawnEnemy));
    }

    void SpawnEnemy()
    {
        if (totalSpawned >= maxEnemies)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        Vector3 spawnPos = GetSpawnPosition();

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        totalSpawned++;
    }

    Vector3 GetSpawnPosition()
    {
        float minX = -8f;
        float maxX = 8f;
        float minY = -4f;
        float maxY = 4f;
        float offset = 2f;

        int side = Random.Range(0, 4);

        float x = 0f;
        float y = 0f;

        switch (side)
        {
            case 0: x = Random.Range(minX, maxX); y = maxY + offset; break;
            case 1: x = Random.Range(minX, maxX); y = minY - offset; break;
            case 2: x = minX - offset; y = Random.Range(minY, maxY); break;
            case 3: x = maxX + offset; y = Random.Range(minY, maxY); break;
        }

        return new Vector3(x, y, 0f);
    }
}