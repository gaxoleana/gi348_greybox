using UnityEngine;
using System.Collections; // Required for Coroutines

public class EnemyHealth : MonoBehaviour
{
    public BossManager bossManager;

    [Header("Health Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Visual Effects")]
    public SpriteRenderer enemySprite;
    public Color damageColor = Color.red;
    public float flashDuration = 0.1f;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        if (enemySprite != null)
        {
            originalColor = enemySprite.color;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took damage! Remaining: " + currentHealth);

        StopCoroutine(FlashRed());
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        if (enemySprite != null)
        {
            enemySprite.color = damageColor;
            yield return new WaitForSeconds(flashDuration);
            enemySprite.color = originalColor;
        }
    }

    void Die()
    {
        if (bossManager != null)
        {
            bossManager.BossDefeated();
        }

        if (transform.parent != null && transform.parent.name == "BossMinion")
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}