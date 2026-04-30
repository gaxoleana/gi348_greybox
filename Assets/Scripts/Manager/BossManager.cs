using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject bossObject;
    public GameObject invisibleWalls;

    private Vector3 initialBossPos;
    private bool bossActive = false;
    private bool isDefeated = false;

    void Start()
    {
        if (bossObject != null)
        {
            bossObject.SetActive(false);
            initialBossPos = bossObject.transform.position;
        }
        invisibleWalls.SetActive(false);
    }

    public void ActivateBoss()
    {
        if (bossActive || isDefeated) return;

        bossActive = true;
        bossObject.SetActive(true);
        invisibleWalls.SetActive(true);

        Debug.Log("Boss Battle Started!");
    }

    public void ResetBoss()
    {
        if (isDefeated) return;

        bossActive = false;

        if (invisibleWalls != null)
            invisibleWalls.SetActive(false);

        if (bossObject != null)
        {
            MiniBoss mb = bossObject.GetComponent<MiniBoss>();
            if (mb != null) mb.ResetBossState();

            FinalBoss fb = bossObject.GetComponent<FinalBoss>();
            if (fb != null) fb.ResetBossState();

            EnemyHealth eHealth = bossObject.GetComponent<EnemyHealth>();
            if (eHealth != null) eHealth.ResetHealth();

            bossObject.transform.position = initialBossPos;

            bossObject.SetActive(false);
        }

        CleanupAndResetEnemies();
    }

    void CleanupAndResetEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            if (enemy.name == "BossMinion")
            {
                Destroy(enemy);
            }
            else if (enemy != bossObject)
            {
                enemy.SetActive(true);
                EnemyHealth eHealth = enemy.GetComponent<EnemyHealth>();
                if (eHealth != null) eHealth.ResetHealth();
            }
        }
    }

    public void BossDefeated()
    {
        isDefeated = true;
        bossActive = false;
        if (invisibleWalls != null) invisibleWalls.SetActive(false);
        Debug.Log("Victory!");
    }
}