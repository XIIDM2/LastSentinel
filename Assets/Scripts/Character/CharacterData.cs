using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("System Settings")]
    [SerializeField] private string _id;
    [SerializeField] private AssetReferenceGameObject _characterGameObject;

    [Header("Character Stats")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _jumpHeight;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackRadius;

    public string ID => _id;
    public AssetReferenceGameObject CharacterPrefab => _characterGameObject;
    public int MaxHealth => _maxHealth;
    public float MovementSpeed => _movementSpeed;
    public int JumpHeight => _jumpHeight;
    public int AttackDamage => _attackDamage;
    public float  AttackCooldown => _attackCooldown;
    public float AttackRadius => _attackRadius;
}
