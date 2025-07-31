using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerAttack : CharacterAttack
{
    [Header("Components")]
    private PlayerInputController _playerInputController;

    private void Awake()
    {
        _playerInputController = GetComponent<PlayerInputController>();
    }

    private void OnEnable()
    {
        _playerInputController.OnAttackPressed += HandleAttackPressed;
        _playerInputController.OnAttackReleased += HandleAttackReleased;
    }

    private void OnDisable()
    {
        _playerInputController.OnAttackPressed -= HandleAttackPressed;
        _playerInputController.OnAttackReleased -= HandleAttackReleased;
    }

    protected override void SetLayersToHit()
    {
        _enemyLayer = LayerMask.GetMask("Enemy", "BossDeath");
    }

    private void HandleAttackPressed()
    {
        IsAttacking = true;
    }

    private void HandleAttackReleased()
    {
        IsAttacking = false;
    }
}
