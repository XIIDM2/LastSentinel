using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int _damage = 15;
    [SerializeField] private float _damageCooldown = 1.5f;

    private float _lastTimeDamaged = float.MinValue;

    private int _playerLayerIndex;

    private void Start()
    {
        _playerLayerIndex = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _playerLayerIndex)
        {
            if (collision.transform.root.TryGetComponent<Health>(out Health health))
            {
                if (Time.time >= _lastTimeDamaged + _damageCooldown)
                {
                    health.TakeDamage(_damage);
                    _lastTimeDamaged = Time.time;
                }
            }
        }
    }
}
