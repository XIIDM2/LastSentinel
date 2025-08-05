using UnityEngine;
using VContainer;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected float _stopDistance = 1.0f;

    [Header("WallCheck")]
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.3f, 0.9f);
    private bool _isStuckInWall => Physics2D.OverlapBox(_wallCheck.position, _wallCheckSize, 0.0f, _groundLayer);

    private LayerMask _groundLayer;
    [Header("CoolDowns")]
    [SerializeField] private float _jumpCoolDown = 1.0f;
    private float _lastJumpTime = float.MinValue;

    [SerializeField] protected float _attackCoolDown;
    protected float _lastAttackTime = float.MinValue;

    [Header("Components")]
    protected EnemyState _currentState;

    protected EnemyDetection _enemyDetection;
    protected EnemyMovement _enemyMovement;
    protected EnemyAttack _enemyAttack;
    private EnemyAnimation _enemyAnimation;
    private Health _health;

    [Inject] protected CharacterData _characterData;

    private void Awake()
    {
        _enemyDetection = GetComponentInChildren<EnemyDetection>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.OnDeath += onDeath;
    }

    private void OnDisable()
    {
        _health.OnDeath -= onDeath;
    }

    protected virtual void Start()
    {
        _groundLayer = LayerMask.GetMask("Ground");

        _attackCoolDown = _characterData.AttackCooldown;

        SetState(EnemyState.Idle);
    }

    private void Update()
    {
        UpdateState();

        switch (_currentState)
        {
            case EnemyState.Idle:
                _enemyMovement.StopMove();
                break;
            case EnemyState.AttackTarget:
                _enemyMovement.StopMove();
                if (Time.time >= _lastAttackTime + _attackCoolDown)
                {
                    _enemyAnimation.OnAttack();
                    _lastAttackTime = Time.time;
                }
                break;
        }
    }

    protected virtual void FixedUpdate()
    {
        switch (_currentState)
        {
            case EnemyState.RunToTarget:
                _enemyMovement.MoveToTarget(_enemyDetection.GetTarget());
                break;
            case EnemyState.Jump:
                if (_enemyMovement.IsGrounded && Time.time >= _lastJumpTime + _jumpCoolDown)
                {
                    _enemyMovement.TryJump();
                    _lastJumpTime = Time.time;     
                }
                break;
        }
    }

    protected virtual void UpdateState()
    {
        if (_currentState == EnemyState.Dead) return;

        if (_enemyDetection.HasTarget)
        {
            if (!_enemyAttack.IsAttacking && Vector2.Distance(_enemyDetection.GetTarget().position, gameObject.transform.position) > _stopDistance)
            {
                SetState(EnemyState.RunToTarget);
            }

            if (Vector2.Distance(_enemyDetection.GetTarget().position, gameObject.transform.position) < _stopDistance)
            {
                SetState(EnemyState.AttackTarget);
            }

        }
        else
        {
            SetState(EnemyState.Idle);
        }

        if (_isStuckInWall)
        {
            SetState(EnemyState.Jump);
        }
    }

    protected void SetState(EnemyState enemyState)
    {
        if (enemyState == _currentState) return;

        _currentState = enemyState;
    }

    private void onDeath()
    {
        SetState(EnemyState.Dead);

        _enemyMovement.StopMove();

        _enemyDetection.enabled = false;
        _enemyMovement.enabled = false;
        _enemyAttack.enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (_wallCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_wallCheck.position, _wallCheckSize);
        }
    }

}
