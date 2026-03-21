using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeSpan = 5f;

    void Start()
    {
        // Destroy bullet after a few seconds so it doesn't clutter the scene
        Destroy(gameObject, lifeSpan);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add damage logic here (e.g., other.GetComponent<Health>().TakeDamage(10);)
            Debug.Log("Player Hit!");
            Destroy(gameObject);
        }
    }
}