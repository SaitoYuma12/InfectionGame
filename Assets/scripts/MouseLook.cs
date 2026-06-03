using UnityEngine;
using UnityEngine.InputSystem;

class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 0.1f;
    [SerializeField] Transform playerBody;

    private InputAction lookAction;

    private float xRotation = 0f;

    private void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>();

        float mouseX = lookValue.x * mouseSensitivity;
        float mouseY = lookValue.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}