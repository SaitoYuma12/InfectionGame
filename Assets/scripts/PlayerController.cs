using UnityEngine;
using UnityEngine.InputSystem;

class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _dashSpeed = 10.0f;
    [SerializeField] private float _jumpPower = 3.0f;


    [SerializeField] private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _dashAction;

    private Rigidbody _rb;

    private Vector2 _moveInput;
    private bool _isGrounded;

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
        _dashAction = _playerInput.actions["Dash"];
        _jumpAction = _playerInput.actions["Jump"];

        _rb = GetComponent<Rigidbody>();
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
