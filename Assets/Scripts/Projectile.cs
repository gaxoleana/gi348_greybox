using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;
    public float lifetime = 3f;

    [Header("Impact Settings")]
    public int damage = 1;

    void Start()
    {
        // Automatically destroy the bullet after a set time if it doesn't hit anything
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Moves the projectile forward based on its own "Up" or "Right" axis
        // If your firePoint's red arrow (X) points at the player, use transform.right
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we hit the player (make sure your Player has the "Player" tag)
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Hit!");
            // Add damage logic here if you have a health script
            Destroy(gameObject);
        }

        // Destroy on impact with the environment/ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}