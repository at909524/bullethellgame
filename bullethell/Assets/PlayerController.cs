using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Used to store the users inputs to make the player move
    private Vector2 direction = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }

    private void OnDrawGizmos()
    {
        //Draw the direction vector as a black line
        Gizmos.color = Color.black;
        Gizmos.DrawLine(Vector3.zero, (Vector3)direction);
    }
}
