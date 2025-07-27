using UnityEngine;
using VContainer;

public class ChangeVisualDirection : MonoBehaviour
{
    private const float thresHold = 0.001f;

    [Inject] private SpriteRenderer spriteRenderer;

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

