using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float jumpForce = 10.0f;

    private float horizontalInput;

    private bool isJumping;

    private PlayerInputController playerInputController;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        playerInputController = GetComponent<PlayerInputController>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        playerInputController.horizontalMovement += HandleHorizontalMovement;
        playerInputController.Jump += HandleJump;
    }

    private void OnDisable()
    {
        playerInputController.horizontalMovement -= HandleHorizontalMovement;
        playerInputController.Jump -= HandleJump;
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocityX = horizontalInput * movementSpeed;

        if (isJumping)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }

        FlipSprite();
    }

    private void HandleHorizontalMovement(float horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }

    private void HandleJump()
    {
        isJumping = true;
    }

    private void FlipSprite()
    {
        if (rigidBody.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rigidBody.linearVelocityX > 0) 
        {
            spriteRenderer.flipX = false;
        }
    }


}
