using UnityEngine;
using VContainer;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private EnemyAttack enemyAttack;
    private Health health;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        health = transform.root.GetComponent<Health>();
        enemyAttack = transform.root.GetComponent<EnemyAttack>();
    }

    private void OnEnable()
    {
        health.HealthDamaged += OnHit;
        health.Death += OnDeath;
    }
    private void OnDisable()
    {
        health.HealthDamaged -= OnHit;
        health.Death -= OnDeath;
    }

    public void ReverseAttackStateEvent()
    {
        enemyAttack.ReverseAttackState();
    }

    public void AttackEvent()
    {
        enemyAttack.Attack();
    }

    public void OnAttack()
    {
        animator.SetTrigger("isAttacking");
    }

    private void OnHit(int damageAmount)
    {
        animator.SetTrigger("isHit");
    }

    private void OnDeath()
    {
        animator.SetBool("isDead", true);
    }
}
