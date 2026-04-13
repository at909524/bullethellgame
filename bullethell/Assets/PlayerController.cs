using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Used to store the users inputs to make the player move
    private Vector2 direction = Vector2.zero;

    private Rigidbody2D rb;

    public float speed = 5f;

    public int health = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()

    {
        //Set the direction vector based on the users input
        //Horizontal
        if(Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }
        else
        {
            direction.x = 0;
        }
        //Vertical
        if(Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }
        else
        {
            direction.y = 0;
        }

        //Make the direction vector have a length of 1 so the player moves at the correct speed
        direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        //(0,0)
        //(1,0)
        //(0,1)
        //(1,1)



        //Move of the character
        rb.MovePosition(rb.position + (direction * speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Subtract the damage the bullet does from the health
        health -= collision.GetComponent<Bullet>().damage;

        //Destroy the bullet
        Destroy(collision.gameObject);
    }
    private void OnDrawGizmos()
    {
        //Draw the direction vector as a black line
        Gizmos.color = Color.black;
        Gizmos.DrawLine(Vector3.zero, (Vector3)direction);
    }
}
