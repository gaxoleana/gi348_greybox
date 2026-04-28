using UnityEngine;

public class Zipline : MonoBehaviour
{
    [Header("Zipline Points")]
    public Transform startPoint;
    public Transform endPoint;
    public float zipSpeed = 18f;

    private bool isZipping = false;
    private GameObject playerRef;
    private Rigidbody2D playerRb;
    private PlayerMovement playerMove;

    void Update()
    {
        if (isZipping && playerRef != null)
        {
            playerRef.transform.position = Vector3.MoveTowards(playerRef.transform.position, endPoint.position, zipSpeed * Time.deltaTime);

            if (Vector3.Distance(playerRef.transform.position, endPoint.position) < 0.1f || Input.GetButtonDown("Jump"))
            {
                StopZip();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isZipping)
        {
            StartZip(other.gameObject);
        }
    }

    void StartZip(GameObject player)
    {
        playerRef = player;
        playerRb = player.GetComponent<Rigidbody2D>();
        playerMove = player.GetComponent<PlayerMovement>();

        isZipping = true;

        if (playerMove != null) playerMove.canMove = false;
        playerRb.bodyType = RigidbodyType2D.Kinematic;
        playerRb.linearVelocity = Vector2.zero;

        playerRef.transform.position = new Vector3(playerRef.transform.position.x, startPoint.position.y, playerRef.transform.position.z);
    }

    void StopZip()
    {
        isZipping = false;

        if (playerMove != null) playerMove.canMove = true;
        playerRb.bodyType = RigidbodyType2D.Dynamic;

        playerRb.linearVelocity = new Vector2(transform.localScale.x * zipSpeed * 0.5f, 5f);

        playerRef = null;
    }
}