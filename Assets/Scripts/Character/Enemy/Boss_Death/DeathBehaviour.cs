using UnityEngine;

public class DeathBehaviour : EnemyBehaviour
{
    protected override void Start()
    {
        _attackCoolDown = _characterData.AttackCooldown;

        SetState(EnemyState.Idle);
    }

    protected override void FixedUpdate()
    {
        switch (_currentState)
        {
            case EnemyState.RunToTarget:
                _enemyMovement.MoveToTarget(_enemyDetection.GetTarget());
                break;
        }
    }

    protected override void UpdateState()
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
    }

    protected override void onDeath()
    {
        base.onDeath();
        Managers.ScenesMananger.Victory();
    }
}
