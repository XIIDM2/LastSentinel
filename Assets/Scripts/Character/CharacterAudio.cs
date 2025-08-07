using UnityEngine;
using VContainer;

public class CharacterAudio : MonoBehaviour
{
    private Health _health;
    private CharacterAttack _characterAttack;
    private CharacterMovement _characterMovement;

    [Inject] private readonly CharacterData _characterData;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAttack = GetComponent<CharacterAttack>();
        _health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        _characterMovement.OnJump += OnJump;
        _characterAttack.OnAttack += OnAttack;
        _health.OnHealthChanged += OnHit;
        _health.OnDeath += OnDeath;
    }
    private void OnDisable()
    {
        _characterMovement.OnJump -= OnJump;
        _characterAttack.OnAttack -= OnAttack;
        _health.OnHealthChanged -= OnHit;
        _health.OnDeath -= OnDeath;
    }

    private void OnJump()
    {
        Managers.AudioManager.PlayClip(_characterData.JumpSound);
    }

    private void OnAttack()
    {
        Managers.AudioManager.PlayClip(_characterData.AttackSound);
    }

    private void OnHit()
    {
        Managers.AudioManager.PlayClip(_characterData.HitSound);
    }

    private void OnDeath()
    {
        Managers.AudioManager.PlayClip(_characterData.DeathSound);
    }
}
