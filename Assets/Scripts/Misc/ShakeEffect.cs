using Unity.Cinemachine;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    private CinemachineImpulseSource _impulseSource;

    private Health _health;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _health = transform.root.GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.OnHealthChanged += OnHit;
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= OnHit;
    }

    private void OnHit()
    {
        Managers.ImpactEffecmanager.HitShakeEffect(_impulseSource);
    }
}
