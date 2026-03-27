using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    void Start()
    {
        // Destroy after 3 seconds so they don't clutter the game
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it hits anything that isn't the Turret itself, destroy it
        if (!collision.CompareTag("Enemy"))
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Ouch! Player Hit!");
            }
            Destroy(gameObject);
        }
    }
}