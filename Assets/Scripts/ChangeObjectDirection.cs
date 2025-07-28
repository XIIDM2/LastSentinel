using UnityEngine;
using VContainer;

public class ChangeObjectDirection : MonoBehaviour
{
    private const float thresHold = 0.001f;
    
    public void FaceDirection(float value)
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

