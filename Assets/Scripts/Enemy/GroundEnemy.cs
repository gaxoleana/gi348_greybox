using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;

    private int currentPointIndex = 0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        Transform target = patrolPoints[currentPointIndex];

        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        if (direction.x > 0.1f) transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < -0.1f) transform.localScale = new Vector3(-1, 1, 1);

        if (Vector2.Distance(transform.position, target.position) < 0.5f)
        {
            currentPointIndex++;

            if (currentPointIndex >= patrolPoints.Length)
            {
                currentPointIndex = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(20f);
            }
        }
    }
}