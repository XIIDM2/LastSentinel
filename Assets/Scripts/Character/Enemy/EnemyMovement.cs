using UnityEngine;
using VContainer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;

    private float jumpForce = 15.0f;
    protected float movementSpeed;

    private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    private LayerMask groundLayer;
    private bool grounded => Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0.0f, groundLayer);

    protected Rigidbody2D rigidBody;
    private ChangeObjectDirection visualDirection;

    [Inject] protected CharacterData characterData;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        visualDirection = GetComponent<ChangeObjectDirection>();
    }

    protected virtual void Start()
    {
        movementSpeed = characterData.MovementSpeed;
        jumpForce = characterData.JumpHeight;

        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        visualDirection.FaceDirection(rigidBody.linearVelocityX);
    }

    public bool GetGroundCheck()
    {
        return grounded;
    }

    public virtual void MoveToTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        rigidBody.linearVelocityX = targetDirection.x * movementSpeed;      

    }

    public virtual void MoveFromTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        rigidBody.linearVelocityX = -targetDirection.x * movementSpeed;

    }

    public virtual void StopMove()
    {
        rigidBody.linearVelocityX = 0.0f;
    }

    public void Jump()
    {
        if (grounded)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
        }
    }
}
