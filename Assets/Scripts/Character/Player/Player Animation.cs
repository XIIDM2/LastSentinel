using UnityEngine;
using VContainer;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator; 
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private Health health;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();

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

    private void Update()
    {
        animator.SetFloat("movementSpeed", Mathf.Abs(playerMovement.HorizontalVelocity));
        animator.SetBool("isOnGround", playerMovement.IsOnGround);
        animator.SetBool("isAttacking", playerAttack.IsAttacking);
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
