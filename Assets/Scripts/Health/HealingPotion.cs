using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int healingAmount = 25;

    private int playerLayerIndex;

    private void Start()
    {
        playerLayerIndex = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayerIndex)
        {
            if (collision.transform.root.TryGetComponent<Health>(out Health health))
            {
                health.HealDamage(healingAmount);
            }

            Destroy(gameObject);
        }
    }
}
