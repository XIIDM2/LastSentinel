using UnityEngine;
using VContainer;

public class EnemyAttack : MonoBehaviour, IPerformAttack
{
    public bool IsAttacking => isAttacking;

    public Transform AttackPoint;

    private int attackDamage;
    private float attackRadius;

    private bool isAttacking;

    private LayerMask playerLayer;

    private CharacterData characterData;
    private EnemyAnimation enemyAnimation;


    [Inject]
    private void Construct(EnemyAnimation enemyAnimation, CharacterData characterData)
    {
        this.enemyAnimation = enemyAnimation;
        this.characterData = characterData;

    }

    private void Start()
    {
        playerLayer = LayerMask.GetMask("Player");

        attackDamage = characterData.AttackDamage;
        attackRadius = characterData.AttackRadius;
    }

    public void Attack()
    {
        enemyAnimation.OnAttack();

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
