using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Health")]
    public int maxHealth = 3;
    public bool isBoss = false;
    public float bossHealthMultiplier = 5f;
    public float bossSpeedMultiplier = 1.2f;
    private bool isDead = false;

    [Header("Rewards")]
    public int pointsOnDeath = 10;



    private int currentHealth;
    private Transform player;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (isBoss)
        {
            maxHealth = Mathf.RoundToInt(maxHealth * bossHealthMultiplier);
            moveSpeed *= bossSpeedMultiplier;
        }

        currentHealth = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }


    public void TakeDamage(int damage)
    {
        killedByPlayer = true;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (killedByPlayer)
        {
            GameManager.instance.AddPoints(pointsOnDeath);
        }

        WaveManager.instance.EnemyKilled();

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player == null) return;

            if (isBoss)
            {
                player.TakeDamage(999);
            }
            else
            {
                player.TakeDamage(1);
            }


            Die();
        }
    }
    private bool killedByPlayer = false;
}