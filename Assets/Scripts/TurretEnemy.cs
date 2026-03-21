using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float rotationSpeed = 5f;

    private float nextFireTime;

    void Update()
    {
        if (player == null) return;

        // 1. Rotate to face the player (Smoothly)
        Vector3 direction = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // 2. Shooting Logic
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}