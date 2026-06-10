using UnityEngine;
using UnityEngine.InputSystem;

class MouseLook : MonoBehaviour
{
    [SerializeField] float _mouseSensitivity = 0.1f;//マウス感度
    [SerializeField] Transform _playerBody;

    private InputAction _lookAction;

    private float _xRotation = 0f;

    private void Start()
    {
        _lookAction = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;//マウスカーソルを中央に固定
    }

    private void Update()
    {
        Vector2 lookValue = _lookAction.ReadValue<Vector2>();

        float mouseX = lookValue.x * _mouseSensitivity;//マウス入力の取得
        float mouseY = lookValue.y * _mouseSensitivity;

        _xRotation -= mouseY;//上下反転
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);//角度制限

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);//上下回転処理

        _playerBody.Rotate(Vector3.up * mouseX);//プレイヤー左右回転
    }
}