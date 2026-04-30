using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject bossObject;
    public GameObject invisibleWalls;
    public Transform bossSpawnPoint;

    private Vector3 initialBossPos;
    private bool bossActive = false;

    void Start()
    {
        bossObject.SetActive(false);
        invisibleWalls.SetActive(false);
        initialBossPos = bossObject.transform.position;
    }

    public void ActivateBoss()
    {
        if (bossActive) return;

        bossActive = true;
        bossObject.SetActive(true);
        invisibleWalls.SetActive(true);

        Debug.Log("Boss Battle Started!");
    }

    public void ResetBoss()
    {
        if (bossObject != null)
        {
            bossObject.SetActive(false);
            bossObject.transform.position = initialBossPos;

            EnemyHealth eHealth = bossObject.GetComponent<EnemyHealth>();
            if (eHealth != null) eHealth.ResetHealth();
        }

        invisibleWalls.SetActive(false);
        bossActive = false;
    }

    public void BossDefeated()
    {
        bossActive = false;
        invisibleWalls.SetActive(false);

        Debug.Log("Victory! Walls cleared.");
    }
}