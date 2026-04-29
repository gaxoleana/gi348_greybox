using UnityEngine;
using System.Collections;

public class MiniBoss : MonoBehaviour
{
    [Header("Positions")]
    public Transform player;
    public float hoverDistance = 6f;
    public float sideHeightOffset = 2.5f;
    public float moveSpeed = 8f;

    [Header("Combat (3 Fire Points)")]
    public Transform[] firePoints;
    public GameObject bulletPrefab;
    public float bulletSpeed = 12f;
    public int shotsPerBurst = 3;
    public float timeBetweenShots = 0.2f;

    [Header("Visuals")]
    public SpriteRenderer bossSprite;
    public Color chargeColor = Color.red;
    private Color originalColor;

    [Header("Timings")]
    public float chargeTime = 1.0f;
    public float restTimeAfterBurst = 1.0f;

    private Vector2 targetPosition;
    private int positionIndex = 0;

    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;

        if (bossSprite != null) originalColor = bossSprite.color;

        StartCoroutine(BossRoutine());
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    IEnumerator BossRoutine()
    {
        while (true)
        {
            SetNewTargetPosition();

            float timer = 0;
            while (Vector2.Distance(transform.position, targetPosition) > 0.3f && timer < 2f)
            {
                SetNewTargetPosition();
                timer += Time.deltaTime;
                yield return null;
            }

            if (bossSprite != null) bossSprite.color = chargeColor;
            yield return new WaitForSeconds(chargeTime);

            if (bossSprite != null) bossSprite.color = originalColor;
            yield return StartCoroutine(FireBurst());

            yield return new WaitForSeconds(restTimeAfterBurst);
            positionIndex = (positionIndex + 1) % 3;
        }
    }

    void SetNewTargetPosition()
    {
        if (player == null) return;

        float sideHeight = sideHeightOffset;

        switch (positionIndex)
        {
            case 0:
                targetPosition = (Vector2)player.position + (Vector2.up * hoverDistance);
                break;
            case 1:
                targetPosition = (Vector2)player.position + (Vector2.left * hoverDistance) + (Vector2.up * sideHeight);
                break;
            case 2:
                targetPosition = (Vector2)player.position + (Vector2.right * hoverDistance) + (Vector2.up * sideHeight);
                break;
        }
    }

    IEnumerator FireBurst()
    {
        for (int i = 0; i < shotsPerBurst; i++)
        {
            ShootFromPoints();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    void ShootFromPoints()
    {
        foreach (Transform fp in firePoints)
        {
            if (fp == null) continue;
            GameObject bullet = Instantiate(bulletPrefab, fp.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 shootDir = (player.position - fp.position).normalized;
                rb.linearVelocity = shootDir * bulletSpeed;
            }
        }
    }
}