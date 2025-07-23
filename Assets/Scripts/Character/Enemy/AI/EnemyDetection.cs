using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyDetection : MonoBehaviour
{

    [SerializeField] private float radius = 3.0f;
    private Transform target;

    private LayerMask playerLayer;

    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        if (circleCollider == null)
        {
            Debug.LogError($"{gameObject.name}: AI Detection Collider is null");
        }

        circleCollider.radius = radius;

        playerLayer = LayerMask.GetMask("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
