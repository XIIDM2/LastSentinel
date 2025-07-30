using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _jumpHeight;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackRadius;

    public int MaxHealth => _maxHealth;
    public float MovementSpeed => _movementSpeed;
    public int JumpHeight => _jumpHeight;
    public int AttackDamage => _attackDamage;
    public float  AttackCooldown => _attackCooldown;
    public float AttackRadius => _attackRadius;
}
