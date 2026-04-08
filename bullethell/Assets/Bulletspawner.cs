using UnityEngine;

public class Bulletspawner : MonoBehaviour
{
    public Camera cam;
    public GameObject bullet;
    private float spawn_timer = 0;



    // Update is called once per frame
    void Update()
    {
        //Spawn bullets when the timer counts down
        spawn_timer -= Time.deltaTime;
        if(spawn_timer <= 0)
        {
            spawn_timer = 1;
            SpawnBullet();
        }
    }

    public void SpawnBullet()
    {
        GameObject new_bullet = Instantiate(bullet);
        int x_pos, y_pos;

        //Spawn on the sides
        if(Random.value < 0.5f)
        {
            //Spawn on left
            if (Random.value < 0.5f)
            {
                x_pos = 0;
            }
            //Spawn on right
            else
            {
                x_pos = cam.scaledPixelWidth;
            }

            //Spawn at a random height
            y_pos = Random.Range(0, cam.scaledPixelHeight);
        }
        //Spawn on the top/bottom
        else
        {
            //Spawn on bottom
            if (Random.value < 0.5f)
            {
                y_pos = 0;
            }
            //Spawn on top
            else
            {
                y_pos = cam.scaledPixelHeight;
            }

            //Spawn at a random width
            x_pos = Random.Range(0, cam.scaledPixelWidth);


        }
        //Convert the x/y_pos to the world space from screen space
        Vector3 spawn_point = new Vector3(x_pos, y_pos, 0);
        spawn_point = cam.ScreenToWorldPoint(spawn_point);
        spawn_point.z = 0;

        //Move the bullet to the spawn point
        new_bullet.transform.position = spawn_point;
    }

}
