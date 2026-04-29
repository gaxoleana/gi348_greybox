using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image healthBarFill;

    [Header("Visual Feedback")]
    public SpriteRenderer playerSprite;
    public Color damageColor = Color.red;
    public float flashDuration = 0.15f;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();

        if (playerSprite != null) originalColor = playerSprite.color;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateUI();

        StopCoroutine(FlashRed());
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        if (playerSprite != null)
        {
            playerSprite.color = damageColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = originalColor;
        }
    }

    void UpdateUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");

        MiniBossManager bm = FindFirstObjectByType<MiniBossManager>();
        if (bm != null) bm.ResetBoss();

        CheckpointManager.instance.RespawnPlayer(gameObject);

        currentHealth = maxHealth;
        UpdateUI();
    }
}