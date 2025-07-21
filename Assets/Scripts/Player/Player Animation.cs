using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        player.animator.SetFloat("movementSpeed", Mathf.Abs(player.playerMovement.HorizontalVelocity));
        player.animator.SetBool("isOnGround", player.playerMovement.IsOnGround);
        player.animator.SetBool("isAttacking", player.playerAttack.IsAttacking);
    }
}
