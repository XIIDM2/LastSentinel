using UnityEngine;
using VContainer;

public class CharacterMovement : MonoBehaviour
{
    [Header("Ground")]
    public bool IsGrounded => isGrounded;
    [SerializeField] protected Transform groundCheckTransform;
    [SerializeField] protected Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    protected bool isGrounded => 
    (
        groundCheckTransform != null && 
        Physics2D.OverlapBox(groundCheckTransform.position, groundBoxSize, 0.0f, groundLayer)
    );

    protected LayerMask groundLayer;

    [Header("Jump")]
    [SerializeField] protected float jumpForce = 15.0f;

    [Header("Movement")]
    [SerializeField] protected float movementSpeed;
    public Vector2 Velocity
    {
        get
        {
            return rigidBody.linearVelocity;
        }
        protected set
        {
            rigidBody.linearVelocity = value;
        }
    }


    [Header("Components")]
    protected Rigidbody2D rigidBody;
    protected ChangeObjectDirection characterDirection;

    [Inject] protected readonly CharacterData characterData;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        characterDirection = GetComponent<ChangeObjectDirection>();
    }

    private void Start()
    {
        movementSpeed = characterData.MovementSpeed;
        jumpForce = characterData.JumpHeight;

        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        characterDirection.FaceDirection(rigidBody.linearVelocityX);
    }

    public void TryJump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(groundCheckTransform.position, groundBoxSize);
        }
    }
}
