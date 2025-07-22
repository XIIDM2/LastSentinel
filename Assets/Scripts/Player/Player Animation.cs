using UnityEngine;
using VContainer;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator; 
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private Health health;

    [Inject]
    private void Construct(Animator animator, PlayerMovement playerMovement, PlayerAttack playerAttack, Health health)
    {
        this.animator = animator;
        this.playerMovement = playerMovement;
        this.playerAttack = playerAttack;
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
