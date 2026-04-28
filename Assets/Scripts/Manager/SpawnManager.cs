using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("Settings")]
    public Transform currentSpawnPoint;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RespawnPlayer(GameObject player)
    {
        if (currentSpawnPoint != null)
        {
            player.transform.position = currentSpawnPoint.position;

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;

            Debug.Log("Player Respawned at: " + currentSpawnPoint.name);
        }
    }

    public void UpdateSpawnPoint(Transform newPoint)
    {
        currentSpawnPoint = newPoint;
    }
}