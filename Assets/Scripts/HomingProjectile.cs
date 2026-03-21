using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public Transform target;
    public float speed = 8f;
    public float rotateSpeed = 200f; // How "sharp" the turn is
    public float lifeSpan = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Find the player automatically if not assigned
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }

        Destroy(gameObject, lifeSpan);
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            // If player is gone, just fly straight
            rb.linearVelocity = transform.forward * speed;
            return;
        }

        // 1. Calculate direction to player
        Vector3 direction = target.position - rb.position;
        direction.Normalize();

        // 2. Calculate the rotation amount
        Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);

        // 3. Apply torque (rotation force) to face the player
        rb.angularVelocity = rotateAmount * rotateSpeed * Time.fixedDeltaTime;

        // 4. Move forward
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Homing Missile Hit!");
            Destroy(gameObject);
        }
    }
}