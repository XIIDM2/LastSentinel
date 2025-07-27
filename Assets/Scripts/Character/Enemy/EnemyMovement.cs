using UnityEngine;
using VContainer;

public class EnemyMovement : MonoBehaviour
{
    public bool IsMoving => rigidBody.linearVelocityX <= 0.01f;
    public bool IsOnGround => isOnGround;

    [SerializeField] private Transform groundCheck;

    private float jumpForce = 15.0f;
    private float movementSpeed;

    private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    private LayerMask groundLayer;
    private bool isOnGround => Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0.0f, groundLayer);

    private Rigidbody2D rigidBody;
    private ChangeVisualDirection visualDirection;

    [Inject] private CharacterData characterData;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        visualDirection = GetComponent<ChangeVisualDirection>();
    }

    private void Start()
    {
        movementSpeed = characterData.MovementSpeed;
        jumpForce = characterData.JumpForce;

        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        visualDirection.FlipSpriteDirection(rigidBody.linearVelocityX);
    }

    public void MoveToTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        rigidBody.linearVelocityX = targetDirection.x * movementSpeed;      

    }

    public void MoveFromTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        rigidBody.linearVelocityX = -targetDirection.x * movementSpeed;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }
}
