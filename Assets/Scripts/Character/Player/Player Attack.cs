using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerAttack : MonoBehaviour, IPerformAttack
{
    public bool IsAttacking => isAttacking;

    public Transform AttackPoint;

    private int attackDamage;
    private float attackRadius;

    private LayerMask enemyLayer;

    private bool isAttacking;

    private PlayerInputController playerInputController;
    private CharacterData characterData;

    [Inject]
    private void Construct(PlayerInputController playerInputController, CharacterData characterData)
    {
        this.playerInputController = playerInputController;
        this.characterData = characterData;
    }

    private void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");

        attackDamage = characterData.AttackDamage;
        attackRadius = characterData.AttackRadius;
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
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(AttackPoint.position, attackRadius, enemyLayer);

        for (int i = 0; i < enemiesHit.Length; i++)
        {
            if (enemiesHit[i].TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(attackDamage);
            }
        }
    }
}
