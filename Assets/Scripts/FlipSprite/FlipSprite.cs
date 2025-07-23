using UnityEngine;
using VContainer;

public class FlipSprite : MonoBehaviour
{
    private float thresHold = 0.001f;
    protected Vector3 attackPointLocalPosition;

    private SpriteRenderer spriteRenderer;

    [Inject]
    private void Construct(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }

    public void FlipSpriteDirection(float value)
    {
        Vector3 transformScale = transform.localScale;

        if (value < -thresHold)
        {
            transformScale.x = -Mathf.Abs(transformScale.x);
        }
        else if (value > thresHold)
        {
            transformScale.x = Mathf.Abs(transformScale.x);
        }

        transform.localScale = transformScale;
    }
}

