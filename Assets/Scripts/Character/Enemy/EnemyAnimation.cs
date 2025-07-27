using UnityEngine;
using VContainer;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private Health health;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        health = GetComponent<Health>();
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
