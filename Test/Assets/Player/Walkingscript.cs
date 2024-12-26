using UnityEngine;

public class Walkingscript : MonoBehaviour
{
    public Transform playerCamera;
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public float cameraRotationSpeed = 2f;
    public float cameraFollowDistance = 5f;

    private void Update()
    {
        HandlePlayerMovement();
        HandleCameraRotation();
    }

    private void HandlePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotation);
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        playerCamera.Rotate(Vector3.up, mouseX * cameraRotationSpeed);
    }

    private void LateUpdate()
    {
        // Calculate the desired camera position based on player's position and camera offset
        Vector3 desiredPosition = transform.position - transform.forward * cameraFollowDistance + Vector3.up * 2f;
        
        // Smoothly move the camera towards the desired position
        playerCamera.position = Vector3.Lerp(playerCamera.position, desiredPosition, Time.deltaTime * 10f);

        // Make the camera always look at the player
        playerCamera.LookAt(transform.position + transform.forward * cameraFollowDistance);
    }
}
