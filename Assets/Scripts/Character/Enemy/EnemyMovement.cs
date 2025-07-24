using UnityEngine;
using VContainer;

public class EnemyMovement : MonoBehaviour
{
    public bool isMoving => rigidBody.linearVelocityX <= 0.01f;
    public bool IsOnGround => isOnGround;

    [SerializeField] private Transform groundCheck;

    private float jumpForce = 15.0f;
    private float movementSpeed;

    private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    private LayerMask groundLayer;
    private bool isOnGround => Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0.0f, groundLayer);

    private Rigidbody2D rigidBody;
    private FlipSprite flipSprite;
    private CharacterData characterData;

    [Inject]
    private void Construct(Rigidbody2D rigidBody, FlipSprite flipSprite)
    {
        this.rigidBody = rigidBody;
        this.flipSprite = flipSprite;
    }

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    public void InitData(CharacterData characterData)
    {
        this.characterData = characterData;

        movementSpeed = characterData.MovementSpeed;
        jumpForce = characterData.JumpForce;
    }

    private void Update()
    {
        flipSprite.FlipSpriteDirection(rigidBody.linearVelocityX);
    }

    public void MoveToTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        rigidBody.linearVelocityX = targetDirection.x * movementSpeed;      

    }

    public void StopMove()
    {
        rigidBody.linearVelocityX = 0.0f;
    }

    public void Jump()
    {
        if (isOnGround)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


}
