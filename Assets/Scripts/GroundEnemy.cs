using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] patrolPoints; // Drag 2 empty GameObjects here
    public float moveSpeed = 2f;

    private int currentPointIndex = 0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure the zombie doesn't fall over
        rb.freezeRotation = true;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // 1. Get the target point
        Transform target = patrolPoints[currentPointIndex];

        // 2. Calculate direction
        Vector2 direction = (target.position - transform.position).normalized;

        // 3. Move the zombie using velocity
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // 4. Flip the sprite to face the movement direction
        if (direction.x > 0.1f) transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < -0.1f) transform.localScale = new Vector3(-1, 1, 1);

        // 5. Check if we reached the point (using a small distance buffer)
        if (Vector2.Distance(transform.position, target.position) < 0.5f)
        {
            // Switch to the next point
            currentPointIndex++;

            // Loop back to the start if we reach the end of the array
            if (currentPointIndex >= patrolPoints.Length)
            {
                currentPointIndex = 0;
            }
        }
    }
}