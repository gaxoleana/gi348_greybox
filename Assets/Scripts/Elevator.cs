using UnityEngine;

public class SimpleElevator : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float targetHeight = 10f;
    private bool isMoving = false;
    private Vector3 startPosition;
    private bool playerOnElevator = false;
    private GameObject playerRef;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (playerOnElevator && !isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartElevator();
            }
        }

        if (isMoving)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

            if (transform.position.y >= startPosition.y + targetHeight)
            {
                StopElevator();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRef = collision.gameObject;
            playerOnElevator = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnElevator = false;
            if (!isMoving) playerRef = null;
        }
    }

    void StartElevator()
    {
        isMoving = true;
        if (playerRef != null)
        {
            playerRef.transform.SetParent(transform);

            PlayerController movement = playerRef.GetComponent<PlayerController>();
            if (movement != null) movement.canMove = false;
        }
    }

    void StopElevator()
    {
        isMoving = false;

        if (playerRef != null)
        {
            playerRef.transform.SetParent(null);

            PlayerController movement = playerRef.GetComponent<PlayerController>();
            if (movement != null) movement.canMove = true;
        }

        playerOnElevator = false;
        playerRef = null;
    }
}