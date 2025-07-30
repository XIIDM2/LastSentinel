using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    public virtual void MoveToTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        _velocity = new Vector2(targetDirection.x * _movementSpeed, _velocity.y);
    }

    public virtual void MoveFromTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        _velocity = new Vector2(-targetDirection.x * _movementSpeed, _velocity.y);  

    }

    public virtual void StopMove()
    {
        _velocity = Vector2.zero;
    }

}
