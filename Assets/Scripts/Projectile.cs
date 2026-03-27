using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        // Physics-based projectiles handle their own cleanup
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for Player tag or Ground layer
        if (collision.CompareTag("Player"))
        {
            // Trigger player damage here
            Destroy(gameObject);
        }
        else if (((1 << collision.gameObject.layer) & LayerMask.GetMask("Ground")) != 0)
        {
            Destroy(gameObject);
        }
    }
}