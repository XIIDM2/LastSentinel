using UnityEngine;
using VContainer;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Links")]
    public float HorizontalVelocity => rigidBody.linearVelocityX;
    public bool Grounded => grounded;

    [Header("Ground")]
    [SerializeField] private Transform groundCheck;
    private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    private LayerMask groundLayer;
    private bool grounded => Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0.0f, groundLayer);
    private float lastGroundedTime;

    [Header("Jump")]
    private float jumpForce = 15.0f;
    private float lastJumpPressedTime = float.MinValue;
    private float jumpBufferTime = 0.2f;
    private bool isJumpBuffered => Time.time - lastJumpPressedTime <= jumpBufferTime;

    [Header("Coyote Jump")]
    private float coyoteJumpTime = 0.2f;
    private bool canCoyoteJump => (Time.time - lastGroundedTime) <= coyoteJumpTime;

    private bool jumped;

    [Header("Movement")]
    private float movementSpeed;
    private float horizontalInputValue;

    [Header("Components")]
    private Rigidbody2D rigidBody;
    private ChangeObjectDirection playerDirection;
    private PlayerInputController playerInputController;

    [Inject] private CharacterData characterData;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        playerInputController = GetComponent<PlayerInputController>();
        playerDirection = GetComponent<ChangeObjectDirection>();
    }

    private void Start()
    {
        movementSpeed = characterData.MovementSpeed;
        jumpForce = characterData.JumpHeight;

        groundLayer = LayerMask.GetMask("Ground"); 
    }

    private void OnEnable()
    {
        playerInputController.horizontalInputValue += HandleHorizontalInputValue;
        playerInputController.OnJumpPressed += HandleJumpPressed;
    }

    private void OnDisable()
    {
        playerInputController.horizontalInputValue -= HandleHorizontalInputValue;
        playerInputController.OnJumpPressed -= HandleJumpPressed;
    }

    private void Update()
    {
        playerDirection.FaceDirection(rigidBody.linearVelocityX);
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocityX = horizontalInputValue * movementSpeed;

        if (grounded)
        {
            lastGroundedTime = Time.time;
            jumped = false;
        }

        if (!jumped && isJumpBuffered && (grounded || canCoyoteJump))
        {
            lastJumpPressedTime = float.MinValue;
            TryJump();
            jumped = true;
        }
    }

    private void TryJump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void HandleHorizontalInputValue(float horizontalInputValue)
    {
        this.horizontalInputValue = horizontalInputValue;
    }

    private void HandleJumpPressed()
    {
        lastJumpPressedTime = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }
}
