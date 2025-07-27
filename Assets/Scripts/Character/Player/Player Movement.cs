using UnityEngine;
using VContainer;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Links")]
    public float HorizontalVelocity => rigidBody.linearVelocityX;
    public bool IsOnGround => isOnGround;
    public bool IsJumping => isJumping;
    public bool IsMoving => isMoving;

    [Header("Ground")]
    [SerializeField] private Transform groundCheck;
    private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    private LayerMask groundLayer;
    private bool isOnGround => Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0.0f, groundLayer);

    [Header("Jump")]
    private float jumpForce = 15.0f;
    private bool isJumping;

    [Header("Movement")]
    private float movementSpeed;
    private float horizontalInput;
    private bool isMoving => Mathf.Abs(rigidBody.linearVelocityX) > 0.01;

    [Header("Components")]
    private Rigidbody2D rigidBody;
    private ChangeVisualDirection changeVisualDirection;
    private PlayerInputController playerInputController;

    [Inject] private CharacterData characterData;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerInputController = GetComponent<PlayerInputController>();
        changeVisualDirection = GetComponent<ChangeVisualDirection>();
    }

    private void Start()
    {
        movementSpeed = characterData.MovementSpeed;
        jumpForce = characterData.JumpForce;

        groundLayer = LayerMask.GetMask("Ground"); 
    }

    private void OnEnable()
    {
        playerInputController.horizontalMovement += HandleHorizontalMovement;
        playerInputController.OnJumpPressed += HandleJumpPressed;
        playerInputController.OnJumpReleased += HandleJumpReleased;
    }

    private void OnDisable()
    {
        playerInputController.horizontalMovement -= HandleHorizontalMovement;
        playerInputController.OnJumpPressed -= HandleJumpPressed;
        playerInputController.OnJumpReleased -= HandleJumpReleased;
    }

    private void Update()
    {
        changeVisualDirection.FlipSpriteDirection(rigidBody.linearVelocityX);
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocityX = horizontalInput * movementSpeed;

        if (isOnGround && isJumping)
        {
            TryJump();
        }
    }

    private void TryJump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void HandleHorizontalMovement(float horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }

    private void HandleJumpPressed()
    {
        isJumping = true;
    }

    private void HandleJumpReleased()
    {
        isJumping = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }
}
