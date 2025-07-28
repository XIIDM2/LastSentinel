using UnityEngine;

public class EnemyAnimation : CharacterAnimation
{
    private EnemyAttack enemyAttack;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        enemyAttack = characterAttack as EnemyAttack;
    }

    public void ReverseAttackStateEvent()
    {
        enemyAttack.ReverseAttackState();
    }

    public void OnAttack()
    {
        animator.SetTrigger("isAttacking");
    }
}
