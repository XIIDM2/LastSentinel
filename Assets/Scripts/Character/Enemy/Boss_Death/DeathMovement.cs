using UnityEngine;

public class DeathMovement : EnemyMovement
{
    protected override void Start()
    {
        movementSpeed = characterData.MovementSpeed;
    }

    public override void MoveToTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        rigidBody.linearVelocity = targetDirection * movementSpeed;

    }

    public override void MoveFromTarget(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        rigidBody.linearVelocity = -targetDirection * movementSpeed;

    }

    public override void StopMove()
    {
        rigidBody.linearVelocity = Vector2.zero;
    }
}
