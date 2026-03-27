using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    [Header("Detection Settings")]
    public Transform player;
    public float range = 8f;

    [Header("Firing Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;

    private float nextFireTime;

    void Update()
    {
        if (player == null) return;

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Shoot if player is within range and cooldown is over
        if (distanceToPlayer <= range && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Create the bullet at the FirePoint's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Aim the bullet toward the player
        Vector2 direction = (player.position - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f;
    }
}