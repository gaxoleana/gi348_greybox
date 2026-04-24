using UnityEngine;

public class SimpleElevator : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float targetHeight = 10f;
    private bool isMoving = false;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
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
            collision.transform.SetParent(transform);

            PlayerMovement movement = collision.gameObject.GetComponent<PlayerMovement>();
            if (movement != null) movement.canMove = false;

            isMoving = true;
        }
    }

    void StopElevator()
    {
        isMoving = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.SetParent(null);
            player.GetComponent<PlayerMovement>().canMove = true;
        }
    }
}