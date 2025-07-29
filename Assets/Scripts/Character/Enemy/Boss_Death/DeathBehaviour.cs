using UnityEngine;

public class DeathBehaviour : EnemyBehaviour
{
    protected override void Start()
    {
        attackCoolDown = characterData.AttackCooldown;

        SetState(EnemyState.Idle);
    }

    protected override void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.RunToTarget:
                enemyMovement.MoveToTarget(enemyDetection.GetTarget());
                break;
        }
    }

    protected override void UpdateState()
    {
        if (currentState == EnemyState.Dead) return;

        if (enemyDetection.HasTarget)
        {
            if (!enemyAttack.IsAttacking && Vector2.Distance(enemyDetection.GetTarget().position, gameObject.transform.position) > stopDistance)
            {
                SetState(EnemyState.RunToTarget);
            }

            if (Vector2.Distance(enemyDetection.GetTarget().position, gameObject.transform.position) < stopDistance)
            {
                SetState(EnemyState.AttackTarget);
            }

        }
        else
        {
            SetState(EnemyState.Idle);
        }
    }
}
