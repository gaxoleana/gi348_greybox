using UnityEngine;
using System.Collections;

public class FinalBoss : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public Transform[] groundEnemySpawnPoints;
    public GameObject groundEnemyPrefab;

    [Header("Phase Settings")]
    public float phase2Threshold = 0.3f;
    public Transform phase2Station;
    private bool isPhase2 = false;

    [Header("Combat Stats")]
    public float phase1FireRate = 1.5f;
    public float phase2FireRate = 0.1f;
    public float spawnInterval = 5f;

    private EnemyHealth health;
    private float nextFireTime;
    private float nextSpawnTime;
    private float clockwiseAngle = 0f;

    void Start()
    {
        health = GetComponent<EnemyHealth>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        if (!isPhase2 && health.GetCurrentHealthPercentage() <= phase2Threshold)
        {
            StartPhase2();
        }

        if (!isPhase2) HandlePhase1();
        else HandlePhase2();
    }

    void HandlePhase1()
    {
        if (Time.time >= nextFireTime)
        {
            foreach (Transform fp in firePoints) { Shoot(fp, (player.position - fp.position).normalized); }
            nextFireTime = Time.time + phase1FireRate;
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnGroundEnemies();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnGroundEnemies()
    {
        foreach (Transform sp in groundEnemySpawnPoints)
        {
            if (sp != null) Instantiate(groundEnemyPrefab, sp.position, Quaternion.identity);
        }
    }

    void HandlePhase2()
    {
        transform.position = Vector2.MoveTowards(transform.position, phase2Station.position, 5f * Time.deltaTime);

        if (Time.time >= nextFireTime)
        {
            clockwiseAngle += 15f;
            Vector2 dir = new Vector2(Mathf.Cos(clockwiseAngle * Mathf.Deg2Rad), Mathf.Sin(clockwiseAngle * Mathf.Deg2Rad));
            foreach (Transform fp in firePoints) { Shoot(fp, dir); }
            nextFireTime = Time.time + phase2FireRate;
        }
    }

    void StartPhase2()
    {
        isPhase2 = true;
        Debug.Log("FINAL PHASE ACTIVATED!");
    }

    void Shoot(Transform point, Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = direction * 15f;
    }
}