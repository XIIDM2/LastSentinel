using UnityEngine;

public class DeathMovement : EnemyMovement
{
    public override void MoveToTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        _velocity = targetDirection * _movementSpeed;
    }

    public override void MoveFromTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        _velocity = -targetDirection * _movementSpeed;
    }

    public override void StopMove()
    {
        _velocity = Vector2.zero;
    }
}
