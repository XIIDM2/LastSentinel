using UnityEngine;
using VContainer;

public class EnemyAttack : CharacterAttack
{
    protected override void SetLayersToHit()
    {
        _enemyLayer = LayerMask.GetMask("Player");
    }

    public void ReverseAttackState()
    {
        IsAttacking = !IsAttacking;
    }

}
