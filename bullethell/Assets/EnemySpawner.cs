using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnInterval = 2f;
    public int maxEnemies = 10;

    private int totalSpawned = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Stop spawning permanently after reaching max
        if (totalSpawned >= maxEnemies)
        {
            CancelInvoke(nameof(SpawnEnemy)); // stop the spawner completely
            return;
        }

        Vector3 spawnPos = GetSpawnPosition();

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        totalSpawned++; // track total spawned
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