using UnityEngine;
using VContainer;

public class EnemyAttack : CharacterAttack
{
    protected override void SetLayersToHit()
    {
        enemyLayer = LayerMask.GetMask("Player");
    }

    public void ReverseAttackState()
    {
        isAttacking = !isAttacking;
    }

}
