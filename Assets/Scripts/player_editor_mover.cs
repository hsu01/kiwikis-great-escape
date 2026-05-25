using UnityEngine;
// Make sure to include the Input System namespace
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;

    [Header("Look Settings")]
    public float lookSpeed = 0.1f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotationY = rot.y;
        rotationX = rot.x;
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        // New Input System check for Right Mouse Button hold
        if (Mouse.current != null && Mouse.current.rightButton.isPressed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Read delta mouse movement
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            rotationY += mouseDelta.x * lookSpeed;
            rotationX -= mouseDelta.y * lookSpeed;

            rotationX = Mathf.Clamp(rotationX, -85f, 85f);
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void HandleMovement()
    {
        if (Keyboard.current == null) return;

        float moveX = 0f;
        float moveZ = 0f;
        float moveY = 0f;

        // Read keyboard keys directly
        if (Keyboard.current.wKey.isPressed) moveZ = 1f;
        if (Keyboard.current.sKey.isPressed) moveZ = -1f;
        if (Keyboard.current.dKey.isPressed) moveX = 1f;
        if (Keyboard.current.aKey.isPressed) moveX = -1f;

        // Vertical movement
        if (Keyboard.current.eKey.isPressed) moveY = 1f;
        if (Keyboard.current.qKey.isPressed) moveY = -1f;

        Vector3 moveDirection = (transform.forward * moveZ) + (transform.right * moveX) + (transform.up * moveY);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}