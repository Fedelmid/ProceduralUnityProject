using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    float moveSpeed = 4f;
    float maxZoom = 10f;
    float originalView = 60f;
    float zoomSpeed = 5f;

    Vector3 cameraRotation;
    Camera cam;

    void Start ()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Navigate with left mouse click
        if (Input.GetMouseButton(0))
        {
            cameraRotation.x += Input.GetAxis("Mouse X") * moveSpeed;
            cameraRotation.y -= Input.GetAxis("Mouse Y") * moveSpeed;

            // Limit the camera rotation
            cameraRotation.y = Mathf.Clamp(cameraRotation.y, 0f, 45f);

            Quaternion QT = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0);
            transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, QT, Time.deltaTime * 0.5f);
        }

        // Zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && Input.mouseScrollDelta.y > 0)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, maxZoom, Time.deltaTime * zoomSpeed);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0 && Input.mouseScrollDelta.y < 0)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, originalView,Time.deltaTime * zoomSpeed);
        }
    }
}
