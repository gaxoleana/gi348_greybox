using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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

        // Check for death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " destroyed!");

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}