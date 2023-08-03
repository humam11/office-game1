using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Vector3 targetPosition;
    private Vector2 cameraSize;

    private void Start()
    {
        targetPosition = GetRandomPosition();
        cameraSize = GetCameraSize();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the object reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }

        // Clamp object's position to stay within the camera's bounds
        float clampedX = Mathf.Clamp(transform.position.x, -cameraSize.x, cameraSize.x);
        float clampedY = Mathf.Clamp(transform.position.y, -cameraSize.y, cameraSize.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-cameraSize.x, cameraSize.x);
        float y = Random.Range(-cameraSize.y, cameraSize.y);
        return new Vector3(x, y, transform.position.z);
    }

    private Vector2 GetCameraSize()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        return new Vector2(cameraWidth, cameraHeight);
    }
}
