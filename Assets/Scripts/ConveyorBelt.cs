using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Settings")]
    public float beltSpeed = 4f;
    public float switchInterval = 3f;

    private float currentSpeed;
    private float timer;

    [Header("Visuals")]
    public SpriteRenderer beltSprite;
    public Color rightColor = Color.cyan;
    public Color leftColor = Color.magenta;

    void Start()
    {
        currentSpeed = beltSpeed;
        timer = switchInterval;
        UpdateVisuals();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            FlipDirection();
            timer = switchInterval;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.position += new Vector2(currentSpeed * Time.deltaTime, 0);
        }
    }

    void FlipDirection()
    {
        currentSpeed *= -1f;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        if (beltSprite != null)
        {
            beltSprite.color = (currentSpeed > 0) ? rightColor : leftColor;

            beltSprite.flipX = (currentSpeed < 0);
        }
    }
}