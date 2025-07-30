using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovement : CharacterMovement
{
    [Header("Ground")]
    private float _lastGroundedTime;

    [Header("Buffered Jump")]
    [SerializeField] private float _jumpBufferTime = 0.2f;
    private float _lastJumpPressedTime = float.MinValue;
    private bool _isJumpBuffered => Time.time - _lastJumpPressedTime <= _jumpBufferTime;

    [Header("Coyote Jump")]
    [SerializeField] private float _coyoteJumpTime = 0.2f;
    private bool _canCoyoteJump => (Time.time - _lastGroundedTime) <= _coyoteJumpTime;

    [Header("Jump Cut")]
    [SerializeField, Range(0f, 1f)] private float _jumpCutValue = 0.2f;

    private bool _hasJumped;

    [Header("Movement Input")]
    [SerializeField] private float _horizontalInput;

    [Header("Components")]
    private PlayerInputController _playerInputController;


    protected override void Awake()
    {
        base.Awake();

        _playerInputController = GetComponent<PlayerInputController>();
    }

    private void OnEnable()
    {
        _playerInputController.MovementInput += HandleHorizontalInputValue;
        _playerInputController.OnJumpPressed += HandleJumpPressed;
        _playerInputController.OnJumpReleased += HandleJumpReleased;
    }

    private void OnDisable()
    {
        _playerInputController.MovementInput -= HandleHorizontalInputValue;
        _playerInputController.OnJumpPressed -= HandleJumpPressed;
        _playerInputController.OnJumpReleased -= HandleJumpReleased;
    }


    private void FixedUpdate()
    {
        _velocity =  new Vector2 (_horizontalInput * _movementSpeed, _velocity.y);

        if (_isGrounded)
        {
            _lastGroundedTime = Time.time;
            _hasJumped = false;
        }

        if (!_hasJumped && _isJumpBuffered && (_isGrounded || _canCoyoteJump))
        {
            _lastJumpPressedTime = float.MinValue;
            TryJump();
            _hasJumped = true;
        }
    }

    private void HandleHorizontalInputValue(Vector2 horizontalInputValue)
    {
        _horizontalInput = horizontalInputValue.x;
    }

    private void HandleJumpPressed()
    {
        _lastJumpPressedTime = Time.time;
    }

    private void HandleJumpReleased()
    {
        if (_velocity.y > 0)  _velocity = new Vector2(_velocity.x, _velocity.y * _jumpCutValue);
    }
}
