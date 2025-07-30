using UnityEngine;

public class LoopedParallaxEffect : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _parallaxSpeed;
    [SerializeField] private float _loopWidth;

    private Transform _camera;
    private Vector3 _lastCameraPosition;

    private void Start()
    {
        _camera = Camera.main.transform;
        _lastCameraPosition = _camera.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = _camera.position - _lastCameraPosition;
        _lastCameraPosition = _camera.position;

        transform.position += new Vector3(delta.x * _parallaxSpeed, 0.0f, 0.0f);

        if (Mathf.Abs(_camera.position.x - transform.position.x) > _loopWidth)
        {
            float direction = Mathf.Sign(_camera.position.x - transform.position.x);
            transform.position += new Vector3(_loopWidth * 2 * direction, 0.0f, 0.0f);
        }
    }
}
