using UnityEngine;

public class DeathMovement : EnemyMovement
{
    public override void MoveToTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        Velocity = targetDirection * movementSpeed;
    }

    public override void MoveFromTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        Velocity = -targetDirection * movementSpeed;
    }

    public override void StopMove()
    {
        Velocity = Vector2.zero;
    }
}
