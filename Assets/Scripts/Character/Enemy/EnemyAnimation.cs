using UnityEngine;

public class EnemyAnimation : CharacterAnimation
{
    private EnemyAttack _enemyAttack;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _enemyAttack = _characterAttack as EnemyAttack;
    }

    public void ReverseAttackStateEvent()
    {
        _enemyAttack.ReverseAttackState();
    }

    public void OnAttack()
    {
        _animator.SetTrigger("isAttacking");
    }
}
