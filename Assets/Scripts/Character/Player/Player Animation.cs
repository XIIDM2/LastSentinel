using UnityEngine;

public class PlayerAnimation : CharacterAnimation
{
    private PlayerMovement _playerMovement;

    protected override  void Awake()
    {
        base.Awake();

        _playerMovement = transform.root.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _animator.SetFloat("movementSpeed", Mathf.Abs(_playerMovement.GetVelocity().x));
        _animator.SetFloat("jumpVelocity", _playerMovement.GetVelocity().y);
        _animator.SetBool("isOnGround", _playerMovement.IsGrounded);
        _animator.SetBool("isAttacking", _characterAttack.IsAttacking);
    }
}
