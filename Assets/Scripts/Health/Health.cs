using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class Health : MonoBehaviour
{
    public event UnityAction<int> HealthDamaged;
    public event UnityAction Death;

    private int maxHealth;
    private int currentHealth;

    [Inject] private readonly CharacterData characterData;

    private void Start()
    {
        maxHealth = characterData.MaxHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        HealthDamaged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death?.Invoke();
        }
    }

    public void HealDamage(int amount)
    {
        if (currentHealth <= 0 || currentHealth >= 0) return;

        currentHealth += amount;
    }
}
