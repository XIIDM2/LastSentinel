using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private Animator animator;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("movementSpeed", Mathf.Abs(playerMovement.HorizontalVelocity));
        animator.SetBool("isOnGround",  playerMovement.IsOnGround);
        animator.SetBool("isAttacking", playerAttack.IsAttacking);
    }
}
