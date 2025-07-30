using UnityEngine;
using VContainer;

public class CharacterMovement : MonoBehaviour
{
    [Header("Ground")]
    public bool IsGrounded => _isGrounded;
    [SerializeField] protected Transform _groundCheckTransform;
    [SerializeField] protected Vector2 _groundBoxSize = new Vector2(0.5f, 0.1f);
    protected bool _isGrounded => 
    (
        _groundCheckTransform != null && 
        Physics2D.OverlapBox(_groundCheckTransform.position, _groundBoxSize, 0.0f, _groundLayer)
    );

    protected LayerMask _groundLayer;

    [Header("Jump")]
    [SerializeField] protected float _jumpForce = 15.0f;

    [Header("Movement")]
    [SerializeField] protected float _movementSpeed;
    [property:SerializeField] protected Vector2 _velocity
    {
        get
        {
            return _rigidBody.linearVelocity;
        }
        set
        {
            _rigidBody.linearVelocity = value;
        }
    }


    [Header("Components")]
    protected Rigidbody2D _rigidBody;
    protected ChangeObjectDirection _characterDirection;

    [Inject] protected readonly CharacterData _characterData;

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _characterDirection = GetComponent<ChangeObjectDirection>();
    }

    private void Start()
    {
        _movementSpeed = _characterData.MovementSpeed;
        _jumpForce = _characterData.JumpHeight;

        _groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        _characterDirection.FaceDirection(_rigidBody.linearVelocityX);
    }

    public Vector2 GetVelocity()
    {
        return _velocity;
    }

    public void TryJump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheckTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_groundCheckTransform.position, _groundBoxSize);
        }
    }
}
