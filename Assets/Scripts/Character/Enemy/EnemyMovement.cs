using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    public virtual void MoveToTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        Velocity = new Vector2(targetDirection.x * movementSpeed, Velocity.y);
    }

    public virtual void MoveFromTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        Velocity = new Vector2(-targetDirection.x * movementSpeed, Velocity.y);  

    }

    public virtual void StopMove()
    {
       Velocity = Vector2.zero;
    }

}
