using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    float moveSpeed = 4f;

    float zoom = 10f;
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

        if (Input.GetMouseButton(0))
        {
            cameraRotation.x += Input.GetAxis("Mouse X") * moveSpeed;
            cameraRotation.y -= Input.GetAxis("Mouse Y") * moveSpeed;

            cameraRotation.y = Mathf.Clamp(cameraRotation.y, 0f, 45f);

            Quaternion QT = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0);
            transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, QT, Time.deltaTime * 0.5f);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0 && Input.mouseScrollDelta.y > 0)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * zoomSpeed);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0 && Input.mouseScrollDelta.y < 0)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, originalView,Time.deltaTime * zoomSpeed);
        }
    }
}
