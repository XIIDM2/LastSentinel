using UnityEngine;
using VContainer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float jumpForce = 15.0f;
    private float movementSpeed;

    private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
    private LayerMask groundLayer;
    private bool isOnGround;

    private CharacterData characterData;
    private Rigidbody2D rigidBody;

    [Inject]
    private void Construct(Rigidbody2D rigidBody, CharacterData characterData)
    {
        this.rigidBody = rigidBody;
        this.characterData = characterData;
    }

    private void Start()
    {
        movementSpeed = characterData.MovementSpeed;
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0.0f, groundLayer);
    }

    public void MoveToTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        rigidBody.linearVelocityX = targetDirection.x * movementSpeed;      

    }

    public void Jump()
    {
        if (isOnGround)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


}
