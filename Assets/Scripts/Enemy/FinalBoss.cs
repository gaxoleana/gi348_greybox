using UnityEngine;

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
    public float spawnRange = 15f;

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

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= spawnRange && Time.time >= nextSpawnTime)
        {
            TrySpawnEnemies();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void TrySpawnEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        int minionCount = 0;

        foreach (GameObject enemy in allEnemies)
        {
            if (enemy.name == "BossMinion") minionCount++;
        }

        if (minionCount < 2)
        {
            int needToSpawn = 2 - minionCount;

            for (int i = 0; i < needToSpawn; i++)
            {
                int randomIndex = Random.Range(0, groundEnemySpawnPoints.Length);
                Transform sp = groundEnemySpawnPoints[randomIndex];

                if (sp != null)
                {
                    Vector3 spawnPos = new Vector3(sp.position.x, sp.position.y, 0f);
                    GameObject minion = Instantiate(groundEnemyPrefab, spawnPos, Quaternion.identity);

                    minion.name = "BossMinion";
                }
            }
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

    public void ResetBossState()
    {
        isPhase2 = false;
        clockwiseAngle = 0f;
        nextFireTime = Time.time;
        nextSpawnTime = Time.time;
        Debug.Log("Final Boss Logic Reverted to Phase 1");
    }
}