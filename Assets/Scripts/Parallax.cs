using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float parallaxFactor;

    public float loopRangeX = 18f;
    public float loopRangeY = 12f;

    private Vector3 lastCameraPosition;

    void Start()
    {
        if (cam == null)
        {
            GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCam != null) cam = mainCam.transform;
        }

        if (cam != null)
        {
            lastCameraPosition = cam.position;
        }
    }

    void LateUpdate()
    {
        if (cam == null) return;

        Vector3 deltaMovement = cam.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxFactor;
        lastCameraPosition = cam.position;

        if (cam.position.y - transform.position.y > loopRangeY)
        {
            transform.position = new Vector3(transform.position.x, cam.position.y + loopRangeY, transform.position.z);
        }
        else if (transform.position.y - cam.position.y > loopRangeY)
        {
            transform.position = new Vector3(transform.position.x, cam.position.y - loopRangeY, transform.position.z);
        }

        if (cam.position.x - transform.position.x > loopRangeX)
        {
            transform.position = new Vector3(cam.position.x + loopRangeX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x - cam.position.x > loopRangeX)
        {
            transform.position = new Vector3(cam.position.x - loopRangeX, transform.position.y, transform.position.z);
        }
    }
}