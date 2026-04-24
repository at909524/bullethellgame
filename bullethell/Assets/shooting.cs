using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // assign in Inspector
    public Transform firePoint;       // assign Bulletshootpoint here
    public float bulletForce = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * bulletForce;
    }
}