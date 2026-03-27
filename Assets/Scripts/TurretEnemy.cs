using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    [Header("Detection")]
    public Transform player;
    public float range = 10f;
    public LayerMask lineOfSightMask; // Should include 'Ground' to block vision

    [Header("Weaponry")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float projectileSpeed = 12f;
    public float fireRate = 2f;
    public float rotationSpeed = 5f;

    private float nextFireTime;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= range && HasLineOfSight())
        {
            RotateTowardsPlayer();

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smoothly rotate the turret
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    bool HasLineOfSight()
    {
        // Prevents the turret from shooting you through walls
        Vector2 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, lineOfSightMask);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Use the turret's current facing direction for the shot
        rb.linearVelocity = firePoint.right * projectileSpeed;
    }
}