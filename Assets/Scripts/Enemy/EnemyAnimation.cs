using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Enemy enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy>();

    }
    private void OnEnable()
    {
        enemy.health.HealthDamaged += OnHit;
        enemy.health.Death += OnDeath;
    }
    private void OnDisable()
    {
        enemy.health.HealthDamaged -= OnHit;
        enemy.health.Death -= OnDeath;
    }

    void Update()
    {
        
    }

    private void OnHit(int damageAmount)
    {
        enemy.animator.SetTrigger("isHit");
    }

    private void OnDeath()
    {
        enemy.animator.SetBool("isDead", true);
    }
}
