using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Drag your Player here
    public float smoothTime = 0.25f; // Delay for smoothness
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset to keep camera back

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the target position with offset
            Vector3 targetPosition = target.position + offset;

            // Move the camera smoothly to that position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}