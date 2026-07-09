using UnityEngine;
using UnityEngine.InputSystem;

class PlayerController : MonoBehaviour
{
    [Header("ˆÚ“®")]
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _dashSpeed = 10.0f;
    [SerializeField] private float _jumpPower = 3.0f;

    [Header("‚µ‚á‚ª‚Ý")]
    [SerializeField] private float _usuallySize = 2.0f;
    [SerializeField] private float _crouchSize = 1.0f;
    [SerializeField] private float _usuallyCamera = 1.0f;
    [SerializeField] private float _crouchCamera = 0.5f;

    [Header("PlayerInput,Camera")]
    [SerializeField] private PlayerInput _playerInput;
    public Camera _camera;

    private InputAction _crouchAction;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _dashAction;

    private Rigidbody _rb;
    private CapsuleCollider _capsuleCol;

    private Vector2 _moveInput;
    private bool _isGrounded;

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
        _dashAction = _playerInput.actions["Dash"];
        _jumpAction = _playerInput.actions["Jump"];
        _crouchAction = _playerInput.actions["Crouch"];

        _rb = GetComponent<Rigidbody>();
        _capsuleCol = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        _isGrounded = true;
    }

    private void Update()
    {
       
        _moveInput = _moveAction.ReadValue<Vector2>();
        
        if (_jumpAction.triggered)
        {
            Jump();
        }

        if (_crouchAction.IsPressed())
        {
            _capsuleCol.height = _crouchSize;
            _capsuleCol.center = new Vector3(0, _crouchSize / 2, 0);

            _camera.transform.localPosition = new Vector3(0, _crouchCamera, 0);
        }
        else
        {
            _capsuleCol.height = _usuallySize;
            _capsuleCol.center = new Vector3(0, _usuallySize / 2, 0);

            _camera.transform.localPosition = new Vector3(0, _usuallyCamera, 0);
        }


    }

    private void FixedUpdate()
    {
        Move(_moveInput);
    }

    private void Move(Vector2 input)
    {
        float currentSpeed;

        if (_dashAction.IsPressed() && _isGrounded)
        {
            currentSpeed = _dashSpeed;
        }
        else
        {
            currentSpeed = _moveSpeed;
        }

        Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;

        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }

        Vector3 velocity = new Vector3(moveDirection.x * currentSpeed, _rb.linearVelocity.y,moveDirection.z * currentSpeed);

        _rb.linearVelocity = velocity;
    }

    private void Jump()
    {
        if (!_isGrounded) return;

        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        _isGrounded = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

}
