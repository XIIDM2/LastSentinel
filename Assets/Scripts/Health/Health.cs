using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class Health : MonoBehaviour
{
    public event UnityAction<int> HealthDamaged;
    public event UnityAction Death;

    private int _maxHealth;
    private int _currentHealth;

    [Inject] private readonly CharacterData _characterData;

    private void Start()
    {
        _maxHealth = _characterData.MaxHealth;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= amount;
        HealthDamaged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Death?.Invoke();
        }
    }

    public void HealDamage(int amount)
    {
        if (_currentHealth <= 0 || _currentHealth >= _maxHealth) return;

        _currentHealth += amount;
    }
}
