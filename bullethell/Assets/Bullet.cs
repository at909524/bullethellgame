using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 direction;
    public float speed = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy after 3 seconds
        Destroy(gameObject, 3f);


        //Get comonents to use
        rb = GetComponent<Rigidbody2D>();

        //Set the direction to point at the player
        GameObject player = GameObject.Find("player");
        direction = player.transform.position - transform.position;
        direction = direction.normalized;

        //Add a force to the rigid body to get it to move
        rb.AddForce(direction * speed , ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
