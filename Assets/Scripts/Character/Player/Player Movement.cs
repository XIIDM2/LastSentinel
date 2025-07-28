using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovement : CharacterMovement
{
    [Header("Ground")]
    private float lastGroundedTime;

    [Header("Jump")]
    private float lastJumpPressedTime = float.MinValue;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private bool isJumpBuffered => Time.time - lastJumpPressedTime <= jumpBufferTime;

    [Header("Coyote Jump")]
    [SerializeField] private float coyoteJumpTime = 0.2f;
    private bool canCoyoteJump => (Time.time - lastGroundedTime) <= coyoteJumpTime;

    private bool hasJumped;

    [Header("Movement")]
    [SerializeField] private float horizontalInput;

    [Header("Components")]
    private PlayerInputController playerInputController;


    protected override void Awake()
    {
        base.Awake();

        playerInputController = GetComponent<PlayerInputController>();
    }

    private void OnEnable()
    {
        playerInputController.MovementInput += HandleHorizontalInputValue;
        playerInputController.OnJumpPressed += HandleJumpPressed;
    }

    private void OnDisable()
    {
        playerInputController.MovementInput -= HandleHorizontalInputValue;
        playerInputController.OnJumpPressed -= HandleJumpPressed;
    }


    private void FixedUpdate()
    {
        Velocity =  new Vector2 (horizontalInput * movementSpeed, Velocity.y);

        if (isGrounded)
        {
            lastGroundedTime = Time.time;
            hasJumped = false;
        }

        if (!hasJumped && isJumpBuffered && (isGrounded || canCoyoteJump))
        {
            lastJumpPressedTime = float.MinValue;
            TryJump();
            hasJumped = true;
        }
    }

    private void HandleHorizontalInputValue(Vector2 horizontalInputValue)
    {
        this.horizontalInput = horizontalInputValue.x;
    }

    private void HandleJumpPressed()
    {
        lastJumpPressedTime = Time.time;
    }
}
