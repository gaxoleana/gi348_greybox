using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Stats")]
    public float range = 10f;
    public float fireRate = 2f;
    public float bulletSpeed = 10f;

    [Header("Visuals")]
    public SpriteRenderer turretSprite;
    public Color warningColor = Color.red;
    public float flashDuration = 0.5f;
    private Color originalColor;

    private float nextFireTime;
    private bool isFlashing = false;

    void Start()
    {
        if (turretSprite != null) originalColor = turretSprite.color;
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= range)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            float timeUntilNextShot = nextFireTime - Time.time;

            if (timeUntilNextShot <= flashDuration && !isFlashing)
            {
                StartFlash();
            }

            // 3. Shoot
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            StopFlash();
            nextFireTime = Time.time + (fireRate - flashDuration);
        }
    }

    void StartFlash()
    {
        if (turretSprite == null) return;
        isFlashing = true;
        turretSprite.color = warningColor;
    }

    void StopFlash()
    {
        if (turretSprite == null) return;
        isFlashing = false;
        turretSprite.color = originalColor;
    }

    void Shoot()
    {
        StopFlash();

        Vector2 direction = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}