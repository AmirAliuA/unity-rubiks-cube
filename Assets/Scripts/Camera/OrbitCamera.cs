using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 6f;
    [SerializeField] private float minDistance = 3.5f;
    [SerializeField] private float maxDistance = 10f;

    [SerializeField] private float rotateSensitivity = 0.2f;
    [SerializeField] private float zoomSensitivity = 0.8f;
    [SerializeField] private float smoothTime = 0.08f;

    private Vector2 currentRot;
    private Vector2 targetRot;
    private Vector2 rotVelocity;
    private float zoomVelocity;

    private void LateUpdate()
    {
        HandleMouse();
        // TODO: Handle touch input for mobile, not a priority right now

        currentRot.x = Mathf.SmoothDamp(
            currentRot.x, targetRot.x, ref rotVelocity.x, smoothTime);

        currentRot.y = Mathf.SmoothDamp(
            currentRot.y, targetRot.y, ref rotVelocity.y, smoothTime);

        currentRot.y = Mathf.Clamp(currentRot.y, -75f, 75f);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Quaternion rot = Quaternion.Euler(currentRot.y, currentRot.x, 0);
        transform.position = target.position + rot * Vector3.back * distance;
        transform.LookAt(target);
    }

    private void HandleMouse()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();
            targetRot += delta * rotateSensitivity;
        }

        if (Mouse.current.scroll != null)
        {
            float scroll = Mouse.current.scroll.ReadValue().y;
            distance -= scroll * zoomSensitivity * Time.deltaTime;
        }
    }
}