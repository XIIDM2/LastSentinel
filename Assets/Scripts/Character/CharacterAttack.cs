using UnityEngine;
using VContainer;

public abstract class CharacterAttack : MonoBehaviour
{
    [Header("Links")]
    public bool IsAttacking => _isAttacking;

    [Header("Attack")]
    [SerializeField] private Transform _AttackPoint;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackRadius;

    protected bool _isAttacking;

    protected LayerMask _enemyLayer;

    [Inject] private readonly CharacterData characterData;

    private void Start()
    {
        _attackDamage = characterData.AttackDamage;
        _attackRadius = characterData.AttackRadius;

        SetLayersToHit();
    }

    public void Attack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_AttackPoint.position, _attackRadius, _enemyLayer);

        for (int i = 0; i < enemiesHit.Length; i++)
        {
            if (enemiesHit[i].transform.root.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(_attackDamage);
            }
        }
    }

    protected abstract void SetLayersToHit();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_AttackPoint.position, _attackRadius);
    }
}
