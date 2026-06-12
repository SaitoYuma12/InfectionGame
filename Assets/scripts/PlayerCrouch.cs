using UnityEngine;
using UnityEngine.InputSystem;

class PlayerCrouch : MonoBehaviour
{
    [SerializeField] private float _usuallySize = 2.0f;
    [SerializeField] private float _crouchSize = 1.0f;

    [SerializeField] private PlayerInput _playerInput;

    private InputAction _crouchAction;

    private CapsuleCollider _capsuleCol;


    private void Awake()
    {
        _crouchAction = _playerInput.actions["Crouch"];

        _capsuleCol = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (_crouchAction.IsPressed())
        {
            _capsuleCol.height = _crouchSize;
            _capsuleCol.center = new Vector3(0, _crouchSize / 2, 0);
        }
        else
        {
            _capsuleCol.height = _usuallySize;
            _capsuleCol.center = new Vector3(0, _usuallySize / 2, 0);
        }
    }
}
