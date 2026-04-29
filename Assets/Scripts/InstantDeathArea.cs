using UnityEngine;

public class InstantDeathArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.instance.RespawnPlayer(other.gameObject);

            other.GetComponent<PlayerHealth>().TakeDamage(10f);
        }
    }
}