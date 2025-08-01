using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyDetection : MonoBehaviour
{
    public bool HasTarget => _target != null;

    [SerializeField] private float _radius = 3.0f;

    private Transform _target;

    private int _playerLayerIndex;

    private CircleCollider2D _circleCollider;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _circleCollider.radius = _radius;
        _circleCollider.isTrigger = true;

        _playerLayerIndex = LayerMask.NameToLayer("Player");

        if (_playerLayerIndex == -1)
        {
            Debug.LogError($"Layer 'Player' not found! create Player layer in Tags & Layers. {gameObject.name} - {this}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _playerLayerIndex)
        {
            _target = collision.transform;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == _target) 
        {
            _target = null;
        }
    }

    public Transform GetTarget()
    {
        return _target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
