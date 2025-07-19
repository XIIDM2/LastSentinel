using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    private float thresHold = 0.001f;

    private PlayerMovementController playerMovementController;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        FlipSpriteDirection();
    }

    private void FlipSpriteDirection()
    {
        if (playerMovementController.HorizontalVelocity < -thresHold)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerMovementController.HorizontalVelocity > thresHold)
        {
            spriteRenderer.flipX = false;
        }
    }
}
