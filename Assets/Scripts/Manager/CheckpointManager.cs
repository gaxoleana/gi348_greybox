using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    public Transform lastCheckpoint;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (lastCheckpoint != null)
        {
            player.transform.position = lastCheckpoint.position;

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }
    }
}