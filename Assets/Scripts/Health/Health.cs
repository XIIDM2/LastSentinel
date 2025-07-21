using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public event UnityAction<int> HealthDamaged;
    public event UnityAction Death;

    private int maxHealth;
    private int currentHealth;


    private void Start()
    {
        maxHealth = 100;
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
