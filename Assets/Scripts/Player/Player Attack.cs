using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerAttack : MonoBehaviour, IPerformAttack
{
    public bool IsAttacking => isAttacking;

    public Transform AttackPoint;

    [SerializeField] private float attackRange = 0.5f;

    private LayerMask enemyLayer;

    private bool isAttacking;

    private PlayerInputController playerInputController;

    [Inject]
    private void Construct(PlayerInputController playerInputController)
    {
        this.playerInputController = playerInputController;
    }

    private void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
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

    private void HandleAttackPressed()
    {
        isAttacking = true;
    }

    private void HandleAttackReleased()
    {
        isAttacking = false;
    }

    public void Attack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayer);

        for (int i = 0; i < enemiesHit.Length; i++)
        {
            if (enemiesHit[i].TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(30);
            }
        }
    }
}
