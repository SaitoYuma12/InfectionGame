using UnityEngine;
using UnityEngine.InputSystem;

class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 0.1f;//マウス感度
    [SerializeField] Transform playerBody;

    private InputAction lookAction;

    private float xRotation = 0f;

    private void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;//マウスカーソルを中央に固定
    }

    private void Update()
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>();

        float mouseX = lookValue.x * mouseSensitivity;//マウス入力の取得
        float mouseY = lookValue.y * mouseSensitivity;

        xRotation -= mouseY;//上下反転
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);//角度制限

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);//上下回転処理

        playerBody.Rotate(Vector3.up * mouseX);//プレイヤー左右回転
    }
}