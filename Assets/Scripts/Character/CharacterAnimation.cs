using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    protected Animator _animator;
    protected CharacterAttack _characterAttack;
    private Health _health;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = transform.root.GetComponent<Health>();
        _characterAttack = transform.root.GetComponent<CharacterAttack>();
    }

    private void OnEnable()
    {
        _health.OnHealthChanged += OnHit;
        _health.OnDeath += OnDeath;
    }
    private void OnDisable()
    {
        _health.OnHealthChanged -= OnHit;
        _health.OnDeath -= OnDeath;
    }

    public void AttackEvent()
    {
        _characterAttack.Attack();
    }

    private void OnHit()
    {
        _animator.SetTrigger("isHit");
    }

    private void OnDeath()
    {
        _animator.SetBool("isDead", true);
    }
}
