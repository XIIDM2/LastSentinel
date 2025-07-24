using UnityEngine;
using VContainer;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Links")]
    public float HorizontalVelocity => rigidBody.linearVelocityX;
    public bool IsJumping => isJumping;
    public bool IsMoving => isMoving;

    [Header("Ground")]
    [SerializeField] private Transform groundCheck;
    private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    private LayerMask groundLayer;
    private bool isOnGround;

    [Header("Jump")]
    private float jumpForce = 15.0f;
    private bool isJumping;

    [Header("Movement")]
    private float movementSpeed;
    private float horizontalInput;
    private bool isMoving => Mathf.Abs(rigidBody.linearVelocityX) > 0.01;

    [Header("Components")]
    private Rigidbody2D rigidBody;
    private FlipSprite flipSprite;
    private PlayerInputController playerInputController;

    private CharacterData characterData;

    [Inject]
    private void Construct(Rigidbody2D rigidbody, PlayerInputController playerInputController, FlipSprite flipSprite)
    {
        this.rigidBody = rigidbody;
        this.playerInputController = playerInputController;
        this.flipSprite = flipSprite;
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

    private void Update()
    {
        flipSprite.FlipSpriteDirection(rigidBody.linearVelocityX);
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocityX = horizontalInput * movementSpeed;

        TryJump();
    }

    public void InitData(CharacterData characterData)
    {
        this.characterData = characterData;

        movementSpeed = characterData.MovementSpeed;
        jumpForce = characterData.JumpForce;
    }

    public bool IsOnGround()
    {
        return isOnGround = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0.0f, groundLayer);
    }

    private void TryJump()
    {
        if (IsOnGround() && isJumping) rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
