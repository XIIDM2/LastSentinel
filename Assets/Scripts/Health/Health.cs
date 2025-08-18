using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class Health : MonoBehaviour
{
    public event UnityAction OnHealthChanged;
    public event UnityAction OnDeath;

    private int _maxHealth;
    private int _currentHealth;

    [Inject] private readonly CharacterData _characterData;

    private void Awake()
    {
        _maxHealth = _characterData.MaxHealth;
        _currentHealth = _maxHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= amount;
        OnHealthChanged?.Invoke();

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
        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        
        OnHealthChanged?.Invoke();
    }
}
