using UnityEditor.U2D.Animation;
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
                enemyMovement.MoveToTarget(enemyDetection.Target);
                break;
        }
    }

    protected override void UpdateState()
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
    }
}
