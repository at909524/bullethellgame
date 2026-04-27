using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Health")]
    public int maxHealth = 3;

    [Header("Rewards")]
    public int pointsOnDeath = 10;

    // Shared across all enemies
    public static int enemiesDestroyed = 0;

    private int currentHealth;
    private Transform player;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure it is tagged 'Player'");
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    // Called by bullet
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        enemiesDestroyed++; // 👈 for "Enemies Left" UI

        // 👇 give points
        if (GameManager.instance != null)
        {
            GameManager.instance.AddPoints(pointsOnDeath);
        }

        Destroy(gameObject);
    }

    // Despawn when touching player (no pushback)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemiesDestroyed++; // still counts as removed

            Destroy(gameObject);
        }
    }
}