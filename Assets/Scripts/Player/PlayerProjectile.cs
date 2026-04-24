using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 0.3f;
    public float damage = 20f;

    private float direction = 1f;

    void Start()
    {
        Destroy(gameObject, lifeTime);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (player.transform.localScale.x < 0)
            {
                direction = -1f;
                transform.localScale = new Vector3(-0.3f, 0.3f, 1f);
            }
            else
            {
                direction = 1f;
                transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            }
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth health = collision.GetComponentInParent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}