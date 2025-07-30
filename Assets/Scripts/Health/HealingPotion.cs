using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int _healingAmount = 25;

    private int _playerLayerIndex;

    private void Start()
    {
        _playerLayerIndex = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _playerLayerIndex)
        {
            if (collision.transform.root.TryGetComponent<Health>(out Health health))
            {
                health.HealDamage(_healingAmount);
            }

            Destroy(gameObject);
        }
    }
}
