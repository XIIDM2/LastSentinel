using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerAttack : MonoBehaviour, IPerformAttack
{
    [Header("Links")]
    public bool IsAttacking => isAttacking;

    [Header("Attack")]
    public Transform AttackPoint;
    private int attackDamage;
    private float attackRadius;
    private bool isAttacking;

    private LayerMask enemyLayer;

    [Header("Components")]
    private PlayerInputController playerInputController;

    private CharacterData characterData;

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

    public void InitData(CharacterData characterData)
    {
        this.characterData = characterData;

        attackDamage = characterData.AttackDamage;
        attackRadius = characterData.AttackRadius;
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

    private void HandleAttackPressed()
    {
        isAttacking = true;
    }

    private void HandleAttackReleased()
    {
        isAttacking = false;
    }

}
