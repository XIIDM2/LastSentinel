using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovementController : MonoBehaviour
{
    public float HorizontalVelocity => rigidBody.linearVelocityX;
    public bool IsOnGround => isOnGround;

    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float jumpForce = 15.0f;
    [SerializeField] private Transform groundCheck;

    private float horizontalInput;
    private bool isJumping;

    private Vector2 groundBoxSize = new Vector2(0.87f, 0.1f);
    private LayerMask groundLayer;
    private bool isOnGround;

    private PlayerInputController playerInputController;
    private Rigidbody2D rigidBody;


    private void OnEnable()
    {
        playerInputController = GetComponent<PlayerInputController>();
        rigidBody = GetComponent<Rigidbody2D>();

        playerInputController.horizontalMovement += HandleHorizontalMovement;
        playerInputController.Jump += HandleJump;

        groundLayer = LayerMask.GetMask("Ground");
    }

    private void OnDisable()
    {
        playerInputController.horizontalMovement -= HandleHorizontalMovement;
        playerInputController.Jump -= HandleJump;
    }

    private void Update()
    {
        isOnGround = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0.0f, groundLayer);
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocityX = horizontalInput * movementSpeed;

        if (isOnGround && isJumping)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    private void HandleHorizontalMovement(float horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }

    private void HandleJump()
    {
        isJumping = true;
    }
}
