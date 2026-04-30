using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossManager bossManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossManager.ActivateBoss();
        }
    }
}