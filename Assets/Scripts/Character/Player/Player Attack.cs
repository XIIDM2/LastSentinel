using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerAttack : MonoBehaviour, IPerformAttack
{
    [Header("Links")]
    public bool IsAttacking => isAttacking;

    [Header("Attack")]
    [SerializeField] private Transform AttackPoint;
    private int attackDamage;
    private float attackRadius;
    private bool isAttacking;

    private LayerMask enemyLayer;

    [Header("Components")]
    private PlayerInputController playerInputController;

    [Inject] private CharacterData characterData;

    private void Awake()
    {
        playerInputController = GetComponent<PlayerInputController>();
    }

    private void Start()
    {
        attackDamage = characterData.AttackDamage;
        attackRadius = characterData.AttackRadius;

        enemyLayer = LayerMask.GetMask("Enemy", "BossDeath");
    }

    private void OnEnable()
    {
        playerInputController.OnAttackPressed += HandleAttackPressed;
        playerInputController.OnAttackReleased += HandleAttackReleased;
    }

    private void OnDisable()
    {
        playerInputController.OnAttackPressed -= HandleAttackPressed;
        playerInputController.OnAttackReleased -= HandleAttackReleased;
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

    private void HandleAttackPressed()
    {
        isAttacking = true;
    }

    private void HandleAttackReleased()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRadius);
    }

}
