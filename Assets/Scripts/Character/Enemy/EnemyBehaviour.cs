using UnityEngine;
using VContainer;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected float stopDistance = 1.0f;

    [Header("WallCheck")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize = new Vector2(0.3f, 0.9f);
    private bool isStuckInWall => Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0.0f, groundLayer);

    private LayerMask groundLayer;

    [SerializeField] private float jumpCoolDown = 1.0f;
    private float lastJumpTime = float.MinValue;

    [SerializeField] protected float attackCoolDown;
    protected float lastAttackTime = float.MinValue;

    [Header("Components")]
    protected EnemyState currentState;

    protected EnemyDetection enemyDetection;
    protected EnemyMovement enemyMovement;
    protected EnemyAttack enemyAttack;
    private EnemyAnimation enemyAnimation;
    private Health health;

    [Inject] protected CharacterData characterData;

    private void Awake()
    {
        enemyDetection = GetComponentInChildren<EnemyDetection>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.Death += onDeath;
    }

    private void OnDisable()
    {
        health.Death -= onDeath;
    }

    protected virtual void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");

        attackCoolDown = characterData.AttackCooldown;

        SetState(EnemyState.Idle);
    }

    private void Update()
    {
        UpdateState();

        switch (currentState)
        {
            case EnemyState.Idle:
                enemyMovement.StopMove();
                break;
            case EnemyState.AttackTarget:
                enemyMovement.StopMove();
                if (Time.time >= lastAttackTime + attackCoolDown)
                {
                    enemyAnimation.OnAttack();
                    lastAttackTime = Time.time;
                }
                break;
        }
    }

    protected virtual void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.RunToTarget:
                enemyMovement.MoveToTarget(enemyDetection.Target);
                break;
            case EnemyState.Jump:
                if (enemyMovement.IsGrounded && Time.time >= lastJumpTime + jumpCoolDown)
                {
                    enemyMovement.TryJump();
                    lastJumpTime = Time.time;     
                }
                break;
        }
    }

    protected virtual void UpdateState()
    {
        if (currentState == EnemyState.Dead) return;

        if (enemyDetection.Target != null)
        {
            if (!enemyAttack.IsAttacking && Vector2.Distance(enemyDetection.Target.position, gameObject.transform.position) > stopDistance)
            {
                SetState(EnemyState.RunToTarget);
            }

            if (Vector2.Distance(enemyDetection.Target.position, gameObject.transform.position) < stopDistance)
            {
                SetState(EnemyState.AttackTarget);
            }

        }
        else
        {
            SetState(EnemyState.Idle);
        }

        if (isStuckInWall)
        {
            SetState(EnemyState.Jump);
        }
    }

    protected void SetState(EnemyState enemyState)
    {
        if (enemyState == currentState) return;

        currentState = enemyState;
    }

    private void onDeath()
    {
        SetState(EnemyState.Dead);

        enemyMovement.StopMove();

        enemyDetection.enabled = false;
        enemyMovement.enabled = false;
        enemyAttack.enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (wallCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
        }
    }

}
