using UnityEngine;

public class LoopedParallaxEffectController : MonoBehaviour
{
    [System.Serializable]
    private class ParallaxLayers
    {
        [SerializeField] private Transform _transform;

        [SerializeField, Range(0.0f, 1.0f)] private float _parallaxSpeed;
        [SerializeField] private float _loopWidth;

        public Vector3 Position => _transform.position;
        public float ParallaxSpeed => _parallaxSpeed;
        public float LoopWidth => _loopWidth;

        public void SetPosition(Vector3 position)
        {
            _transform.position += position;
        }

    }

    [SerializeField] private ParallaxLayers[] _parallaxLayers; 
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

        foreach (ParallaxLayers parallaxLayer in _parallaxLayers)
        {
            parallaxLayer.SetPosition(new Vector3(delta.x * parallaxLayer.ParallaxSpeed, 0.0f, 0.0f));

            if (Mathf.Abs(_camera.position.x - parallaxLayer.Position.x) > parallaxLayer.LoopWidth)
            {
                float direction = Mathf.Sign(_camera.position.x - parallaxLayer.Position.x);

                parallaxLayer.SetPosition(new Vector3(parallaxLayer.LoopWidth * 2 * direction, 0.0f, 0.0f));
            }
        }
    }
}
