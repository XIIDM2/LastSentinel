using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerAttack : CharacterAttack
{
    [Header("Components")]
    private PlayerInputController playerInputController;

    private void Awake()
    {
        playerInputController = GetComponent<PlayerInputController>();
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

    protected override void SetLayersToHit()
    {
        enemyLayer = LayerMask.GetMask("Enemy", "BossDeath");
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
