using UnityEngine;
using UnityEngine.InputSystem;

class PlayerCrouch : MonoBehaviour
{
    [SerializeField] private float _usuallySize = 2.0f;
    [SerializeField] private float _CrouchSize = 1.0f;

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
            _capsuleCol.height = _CrouchSize;
        }
        else
        {
            _capsuleCol.height = _usuallySize;
        }
    }
}
