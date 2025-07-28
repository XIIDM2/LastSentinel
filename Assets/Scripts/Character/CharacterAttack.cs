using UnityEngine;
using VContainer;

public abstract class CharacterAttack : MonoBehaviour
{
    [Header("Links")]
    public bool IsAttacking => isAttacking;

    [Header("Attack")]
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRadius;

    protected bool isAttacking;

    protected LayerMask enemyLayer;

    [Inject] private readonly CharacterData characterData;

    private void Start()
    {
        attackDamage = characterData.AttackDamage;
        attackRadius = characterData.AttackRadius;

        SetLayersToHit();
    }

    public void Attack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(AttackPoint.position, attackRadius, enemyLayer);

        for (int i = 0; i < enemiesHit.Length; i++)
        {
            if (enemiesHit[i].transform.root.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    protected abstract void SetLayersToHit();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRadius);
    }
}
