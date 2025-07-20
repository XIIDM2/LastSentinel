using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovement : MonoBehaviour
{
    public float HorizontalVelocity => rigidBody.linearVelocityX;
    public bool IsOnGround => isOnGround;

    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float jumpForce = 15.0f;
    [SerializeField] private Transform groundCheck;

    private float horizontalInput;
    private bool isJumping;

    private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    private LayerMask groundLayer;
    private bool isOnGround;

    private PlayerInputController playerInputController;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        playerInputController = GetComponent<PlayerInputController>();
        rigidBody = GetComponent<Rigidbody2D>();     
    }

    private void Start()
    {
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

    private void FixedUpdate()
    {
        rigidBody.linearVelocityX = horizontalInput * movementSpeed;

        isOnGround = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0.0f, groundLayer);

        if (isOnGround && isJumping)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

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

}
