using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float fireRate = 0.3f;

    public Transform firePointSingle;
    public Transform firePointLeft;
    public Transform firePointRight;

    [HideInInspector]
    public int barrelCount = 1;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (barrelCount == 1)
        {
            FireFromPoint(firePointSingle);
        }
        else if (barrelCount >= 2)
        {
            FireFromPoint(firePointLeft);
            FireFromPoint(firePointRight);
        }
    }

    void FireFromPoint(Transform firePoint)
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