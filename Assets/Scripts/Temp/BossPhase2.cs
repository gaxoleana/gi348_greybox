using UnityEngine;
using System.Collections;

public class BossPhase2 : MonoBehaviour
{
    [Header("Movement (Phase 1)")]
    public float moveDistance = 10f;
    public float moveSpeed = 5f;
    public float waitPhase1 = 10f;

    [Header("Death (Phase 2)")]
    public float waitPhase2 = 15f;

    [Header("Cleanup")]
    public string enemyTag = "Enemy";

    private int currentPhase = 0;
    private bool isWaiting = false;
    private Vector3 destination;

    void Start()
    {
        destination = transform.position + Vector3.down * moveDistance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isWaiting)
        {
            HandleHit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isWaiting)
        {
            HandleHit();
        }
    }

    void HandleHit()
    {
        if (currentPhase == 0)
        {
            currentPhase = 1;
            StartCoroutine(TransitionToPhase2());
        }
        else if (currentPhase == 1)
        {
            currentPhase = 2;
            StartCoroutine(DieAfterDelay());
        }
    }

    IEnumerator TransitionToPhase2()
    {
        isWaiting = true;

        yield return new WaitForSeconds(waitPhase1);

        CleanupEnemies();

        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = destination;

        isWaiting = false;
    }

    IEnumerator DieAfterDelay()
    {
        isWaiting = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color; // Usually your Red
        Color flashColor = Color.white;

        float timer = 0;

        while (timer < waitPhase2)
        {
            if (sr.color == originalColor)
                sr.color = flashColor;
            else
                sr.color = originalColor;

            float flickerSpeed = Mathf.Lerp(0.4f, 0.05f, timer / waitPhase2);

            yield return new WaitForSeconds(flickerSpeed);
            timer += flickerSpeed;
        }

        Destroy(gameObject);
    }

    void CleanupEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}