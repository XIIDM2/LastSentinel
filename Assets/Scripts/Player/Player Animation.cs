using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovementController playerMovementController;
    private Animator animator;


    private void OnEnable()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("movementSpeed", Mathf.Abs(playerMovementController.HorizontalVelocity));
        animator.SetBool("isOnGround", !playerMovementController.IsOnGround);
    }
}
