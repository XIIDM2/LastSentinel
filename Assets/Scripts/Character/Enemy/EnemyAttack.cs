using UnityEngine;
using VContainer;

public class EnemyAttack : MonoBehaviour, IPerformAttack
{
    [Header("Links")]
    public bool IsAttacking => isAttacking;

    [Header("Attack")]
    [SerializeField] private Transform AttackPoint;
    private int attackDamage;
    private float attackRadius;
    private bool isAttacking;

    private LayerMask playerLayer;

    [Header("Components")]
    [Inject] private CharacterData characterData;


    private void Start()
    {
        attackDamage = characterData.AttackDamage;
        attackRadius = characterData.AttackRadius;

        playerLayer = LayerMask.GetMask("Player");
    }

    public void Attack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(AttackPoint.position, attackRadius, playerLayer);

        for (int i = 0; i < enemiesHit.Length; i++)
        {
            if (enemiesHit[i].TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRadius);
    }
}
