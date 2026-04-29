using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public MiniBossManager bossManager;

    [Header("Health Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Visual Effects (Optional)")]
    public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took damage! Remaining: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (bossManager != null)
        {
            bossManager.BossDefeated();
        }

        Destroy(gameObject);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}