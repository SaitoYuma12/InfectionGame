using UnityEngine;
using UnityEngine.InputSystem;

class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _dashSpeed = 10.0f;
    [SerializeField] private float _jumpPower = 3.0f;

    private bool _isGrounded;

    [SerializeField] private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _dashAction;

    private Rigidbody _rb;

    private Vector2 _moveInput;

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
        _dashAction = _playerInput.actions["Dash"];
        _jumpAction = _playerInput.actions["Jump"];

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
        _moveInput = _moveAction.ReadValue<Vector2>();
        
        if (_jumpAction.triggered)
        {
            Jump();
        }

       
    }

    private void FixedUpdate()
    {
        Move(_moveInput);
    }

    private void Move(Vector2 input)
    {
        float _currentSpeed;

        if (_dashAction.IsPressed())
        {
            _currentSpeed = _dashSpeed;
        }
        else
        {
            _currentSpeed = _moveSpeed;
        }

        Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;

        Vector3 velocity = new Vector3(moveDirection.x * _currentSpeed, _rb.linearVelocity.y,moveDirection.z * _currentSpeed);

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

}
