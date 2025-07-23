using UnityEngine;
using VContainer;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private Health health;
    private EnemyAttack enemyAttack;

    [Inject]
    private void Construct(Animator animator,  Health health)
    {
        this.animator = animator;
        this.health = health;

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
