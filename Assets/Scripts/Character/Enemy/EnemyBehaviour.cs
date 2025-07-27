using System.Collections;
using UnityEngine;
using VContainer;

public enum EnemyState
{ 
    Idle,
    RunToTarget,
    AttackTarget,
    Jump,
    Dead,
}

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private float stopDistance = 1.0f;

    [Header("WallCheck")]
    [SerializeField] private Transform wallCheck;
    private Vector2 wallCheckSize = new Vector2(0.3f, 0.9f);
    private bool isStuckInWall => Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0.0f, groundLayer);

    private LayerMask groundLayer;

    private float jumpCoolDown = 1.0f;
    private float lastJumpTime = float.MinValue;

    private float attackCoolDown;
    private float lastAttackTime = float.MinValue;

    [Header("Components")]
    private EnemyState currentState;

    private EnemyDetection enemyDetection;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyAnimation enemyAnimation;
    private Health health;

    [Inject] private CharacterData characterData;

    private void Awake()
    {
        enemyDetection = GetComponentInChildren<EnemyDetection>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAnimation = GetComponent<EnemyAnimation>();
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

    private void Start()
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

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.RunToTarget:
                enemyMovement.MoveToTarget(enemyDetection.Target);
                break;
            case EnemyState.Jump:
                if (Time.time >= lastJumpTime + jumpCoolDown)
                {
                    enemyMovement.Jump();
                    lastJumpTime = Time.time;
                }
                break;
        }
    }

    private void UpdateState()
    {
        if (currentState == EnemyState.Dead) return;

        if (enemyDetection.Target != null)
        {
            if (Vector2.Distance(enemyDetection.Target.position, gameObject.transform.position) > stopDistance)
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

    private void SetState(EnemyState enemyState)
    {
        if (enemyState == currentState) return;

        currentState = enemyState;
    }

    private void onDeath()
    {
        SetState(EnemyState.Dead);

        enemyDetection.enabled = false;
        enemyMovement.enabled = false;
        enemyAttack.enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
    }

}
