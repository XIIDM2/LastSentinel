using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    private float thresHold = 0.001f;
    private Vector3 attackPointLocalPosition;

    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        attackPointLocalPosition = playerAttack.AttackPoint.localPosition;
    }

    private void Update()
    {
        FlipSpriteDirection();
    }

    private void FlipSpriteDirection()
    {

        if (playerMovement.HorizontalVelocity < -thresHold)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerMovement.HorizontalVelocity > thresHold)
        {
            spriteRenderer.flipX = false;
        }

        FlipAttackPoint(spriteRenderer.flipX);
    }

    private void FlipAttackPoint(bool facingLeft)
    {
        Vector3 attackPointPosition = attackPointLocalPosition;

        attackPointPosition.x = facingLeft ? -Mathf.Abs(attackPointPosition.x) : Mathf.Abs(attackPointPosition.x);

        playerAttack.AttackPoint.localPosition = attackPointPosition;
    }
}

