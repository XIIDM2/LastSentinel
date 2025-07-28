using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    protected Animator animator;
    protected CharacterAttack characterAttack;
    private Health health;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        health = transform.root.GetComponentInParent<Health>();
        characterAttack = transform.root.GetComponent<CharacterAttack>();
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

    public void AttackEvent()
    {
        characterAttack.Attack();
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
