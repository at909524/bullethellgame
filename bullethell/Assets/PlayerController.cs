using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Health")]
    public int maxHits = 3;
    public int maxUpgradableLives = 10;

    [Header("UI")]
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI enemiesLeftText;

    [Header("Game")]
    public int maxEnemies = 10;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 0.2f;


    private Rigidbody2D rb;
    private Vector2 movement;
    private float fireTimer;
    public int currentHits;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHits = maxHits;

        UpdateLivesUI();
        UpdateEnemiesUI();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        AimAtMouse();
        HandleShooting();
        UpdateEnemiesUI();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    void AimAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void HandleShooting()
    {
        fireTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            
            fireTimer = fireRate;
        }
    }





    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + currentHits;
    }
    public void AddLife(int amount)
    {

        maxHits += amount;


        if (maxHits > maxUpgradableLives)
            maxHits = maxUpgradableLives;


        currentHits += 1;


        if (currentHits > maxHits)
            currentHits = maxHits;

        UpdateLivesUI();
    }

    void UpdateEnemiesUI()
    {
        if (enemiesLeftText != null && WaveManager.instance != null)
        {
            enemiesLeftText.text =
                "Wave: " + WaveManager.instance.currentWave;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHits -= amount;

        UpdateLivesUI();

        if (currentHits <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}