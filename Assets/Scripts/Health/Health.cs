using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class Health : MonoBehaviour
{
    public event UnityAction<int> OnHealthDamaged;
    public event UnityAction OnDeath;

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
        OnHealthDamaged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDeath?.Invoke();
        }
    }

    public void HealDamage(int amount)
    {
        if (_currentHealth <= 0 || _currentHealth >= _maxHealth) return;

        _currentHealth += amount;
    }
}
