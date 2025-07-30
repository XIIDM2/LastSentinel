using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    [SerializeField, Range(0.01f, 0.05f)] private float _parallaxSpeed;

    private Transform _camera;
    private Vector2 _cameraStartPosition;

    private float _cameraDeltaX;

    private Transform[] _transforms;
    private MaterialPropertyBlock[] _matPropBlocks;
    private Renderer[] _renderers;
    private float[] _speed;
    private float _farthestparallaxObject = float.MinValue;

    private void Start()
    {
        _camera = Camera.main.transform;
        _cameraStartPosition = _camera.position;

        int parallaxObjectsCount = transform.childCount;
        _transforms = new Transform[parallaxObjectsCount];
        _matPropBlocks = new MaterialPropertyBlock[parallaxObjectsCount];
        _renderers = new Renderer[parallaxObjectsCount];
        _speed = new float[parallaxObjectsCount];

        for (int i = 0; i < parallaxObjectsCount; i++)
        {
            _transforms[i] = transform.GetChild(i).transform;
            _renderers[i] = _transforms[i].GetComponent<MeshRenderer>();
            _matPropBlocks[i] = new MaterialPropertyBlock();
        }

        ParallaxSpeedCalc(_transforms.Length);
    }

    private void ParallaxSpeedCalc(int parallaxObjectsCount)
    {
        for (int i = 0; i < parallaxObjectsCount; i++)
        {
            float zDistance = _transforms[i].position.z - _camera.position.z;

            if (zDistance > _farthestparallaxObject)
            {
                _farthestparallaxObject = zDistance;
            }
        }

        for (int i = 0; i < parallaxObjectsCount; i++)
        {
            _speed[i] = 1 - (_transforms[i].position.z - _camera.position.z) / _farthestparallaxObject;
        }
    }

    private void LateUpdate()
    {
        _cameraDeltaX = _camera.position.x - _cameraStartPosition.x;
        transform.position = new Vector3(_camera.position.x, _camera.position.y, 0);

        for (int i = 0; i < _transforms.Length; i++)
        {
            float speed = _speed[i] * _parallaxSpeed;
            Vector2 offset = new Vector2(_cameraDeltaX, 0) * speed;

            _renderers[i].GetPropertyBlock(_matPropBlocks[i]);
            _matPropBlocks[i].SetVector("_MainTex_ST", new Vector4(1, 1, offset.x, offset.y));
            _renderers[i].SetPropertyBlock(_matPropBlocks[i]);
        }
    }
}
