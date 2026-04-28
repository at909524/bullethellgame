using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [Header("Wave Settings")]
    public int currentWave = 1;
    public int enemiesPerWave = 10;
    public float waveMultiplier = 1.3f;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public bool gameEnded = false;
    public TextMeshProUGUI winText;
    public bool gameStarted = false;
    public GameObject startButton;

    public int enemiesAlive;



    void Awake()
    {
        instance = this;
    }
    public void StartGame()
    {
        gameStarted = true;

        currentWave = 1;
        enemiesPerWave = 10;
        gameEnded = false;

        Time.timeScale = 1f;

        if (startButton != null)
            startButton.SetActive(false);

        StartWave();
    }
    void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);

        Enemy bossEnemy = boss.GetComponent<Enemy>();

        if (bossEnemy != null)
        {
            bossEnemy.isBoss = true;
        }
    }

    void Start()
    {
        gameEnded = false;
        currentWave = 1;
        enemiesAlive = 0;

        Time.timeScale = 1f;

        StartWave();
    }



    public void StartWave()
    {
        if (!gameStarted) return; 
        if (gameEnded) return;

        EnemySpawner.instance.StopSpawning();

        if (currentWave == 5)
        {
            SpawnBoss();
            enemiesAlive = 1;
            return;
        }

        EnemySpawner.instance.SpawnWave(enemiesPerWave);
        enemiesAlive = enemiesPerWave;
    }

    public void EnemyKilled()
    {
        if (gameEnded) return; 

        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            if (currentWave == 5)
            {
                WinGame();
                return;
            }

            NextWave();
        }
    }
    void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;

        EnemySpawner.instance.StopSpawning();

        Time.timeScale = 0f;

        if (winText != null)
            winText.gameObject.SetActive(true);
    }

    void NextWave()
    {
        currentWave++;

        enemiesPerWave = Mathf.RoundToInt(enemiesPerWave * waveMultiplier);

        Debug.Log("Next Wave: " + currentWave + " | Enemies: " + enemiesPerWave);

        StartWave();
    }
}