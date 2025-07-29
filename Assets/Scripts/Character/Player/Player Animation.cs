using UnityEngine;

public class PlayerAnimation : CharacterAnimation
{
    private PlayerMovement playerMovement;

    protected override  void Awake()
    {
        base.Awake();

        playerMovement = transform.root.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        animator.SetFloat("movementSpeed", Mathf.Abs(playerMovement.GetVelocity().x));
        animator.SetBool("isOnGround", playerMovement.IsGrounded);
        animator.SetBool("isAttacking", characterAttack.IsAttacking);
    }
}
