using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Health")]
    public int maxHits = 3;

    [Header("UI")]
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI enemiesLeftText;

    [Header("Game")]
    public int maxEnemies = 10;

    private int currentHits;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHits = maxHits;

        // Reset enemy counter at start of game
        Enemy.enemiesDestroyed = 0;

        UpdateLivesUI();
        UpdateEnemiesUI();
    }

    void Update()
    {
        // Movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Aim toward mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Update enemies left UI
        UpdateEnemiesUI();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeHit();
        }
    }

    void TakeHit()
    {
        currentHits--;
        UpdateLivesUI();

        if (currentHits <= 0)
        {
            Die();
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentHits;
        }
    }

    void UpdateEnemiesUI()
    {
        if (enemiesLeftText != null)
        {
            int remaining = maxEnemies - Enemy.enemiesDestroyed;

            if (remaining < 0) remaining = 0;

            enemiesLeftText.text = "Enemies Left: " + remaining;
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}