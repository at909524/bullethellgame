using UnityEngine;

public class Bulletspawner : MonoBehaviour
{
    public Camera cam;
    public GameObject bullet;
    private float spawn_timer = 0;

    public int max_bullets_at_a_time = 5;
    public float min_spawn_time = 0.25f;
    public float max_spawn_time = 1f;


    // Update is called once per frame
    void Update()
    {
        //Spawn bullets when the timer counts down
        spawn_timer -= Time.deltaTime;
        if(spawn_timer <= 0)
        {
            //Give the timer a random spawn time
            spawn_timer = Random.Range(min_spawn_time, max_spawn_time);

            //create a variable for th enumber o fubllets to spawn
            int bullets_to_spawn = Random.Range(1, max_bullets_at_a_time + 1);

            //Spawn that numbe of bullets
            for (int i = 0; i < bullets_to_spawn; i++)
            {
                SpawnBullet();
            }
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
