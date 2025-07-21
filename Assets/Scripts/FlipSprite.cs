using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    private float thresHold = 0.001f;
    private Vector3 attackPointLocalPosition;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        attackPointLocalPosition = player.playerAttack.AttackPoint.localPosition;
    }

    private void Update()
    {
        FlipSpriteDirection();
    }

    private void FlipSpriteDirection()
    {

        if (player.playerMovement.HorizontalVelocity < -thresHold)
        {
            player.spriteRenderer.flipX = true;
        }
        else if (player.playerMovement.HorizontalVelocity > thresHold)
        {
            player.spriteRenderer.flipX = false;
        }

        FlipAttackPoint(player.spriteRenderer.flipX);
    }

    private void FlipAttackPoint(bool facingLeft)
    {
        Vector3 attackPointPosition = attackPointLocalPosition;

        attackPointPosition.x = facingLeft ? -Mathf.Abs(attackPointPosition.x) : Mathf.Abs(attackPointPosition.x);

        player.playerAttack.AttackPoint.localPosition = attackPointPosition;
    }
}

