using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class Health : MonoBehaviour, IDamageable
{
    public event UnityAction<int> HealthDamaged;
    public event UnityAction Death;

    private int maxHealth;
    private int currentHealth;

    private CharacterData characterData;

    [Inject]
    private void Construct( CharacterData characterData)
    {
        this.characterData = characterData;
    }

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
}
