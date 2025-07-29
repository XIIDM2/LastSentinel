using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyDetection : MonoBehaviour
{
    public bool HasTarget => target != null;

    [SerializeField] private float radius = 3.0f;

    private Transform target;

    private int playerLayerIndex;

    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        circleCollider.radius = radius;
        circleCollider.isTrigger = true;

        playerLayerIndex = LayerMask.NameToLayer("Player");

        if (playerLayerIndex == -1)
        {
            Debug.LogError($"Layer 'Player' not found! create Player layer in Tags & Layers. {gameObject.name} - {this}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayerIndex)
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == target) 
        {
            target = null;
        }
    }

    public Transform GetTarget()
    {
        return target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
