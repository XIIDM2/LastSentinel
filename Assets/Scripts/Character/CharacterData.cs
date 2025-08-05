using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("System Settings")]
    [SerializeField] private string _id;
    [SerializeField] private AssetReferenceGameObject _characterGameObject;

    [Header("SFX")]
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _moveSound;
    [SerializeField] private AudioClip _jumpSound;

    [Header("Character Stats")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _jumpHeight;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackRadius;

    public string ID => _id;
    public AssetReferenceGameObject CharacterPrefab => _characterGameObject;
    public AudioClip AttackSound => _attackSound;
    public AudioClip HitSound => _hitSound;
    public AudioClip DeathSound => _deathSound;
    public AudioClip MoveSound => _moveSound;
    public AudioClip JumpSound => _jumpSound;
    public int MaxHealth => _maxHealth;
    public float MovementSpeed => _movementSpeed;
    public int JumpHeight => _jumpHeight;
    public int AttackDamage => _attackDamage;
    public float  AttackCooldown => _attackCooldown;
    public float AttackRadius => _attackRadius;
}
