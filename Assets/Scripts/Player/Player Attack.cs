using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerAttack : MonoBehaviour, IPerformAttack
{
    public bool IsAttacking => isAttacking;

    public Transform AttackPoint;

    [SerializeField] private float attackRange = 0.5f;

    private LayerMask enemyLayer;

    private PlayerInputController playerInputController;

    private bool isAttacking;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void OnEnable()
    {
        player.playerInputController.OnAttackPressed += HandleAttackPressed;
        player.playerInputController.OnAttackReleased += HandleAttackReleased;
    }

    private void OnDisable()
    {
        player.playerInputController.OnAttackPressed -= HandleAttackPressed;
        player.playerInputController.OnAttackReleased -= HandleAttackReleased;
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
