using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int jumpHeight;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRadius;

    public int MaxHealth => maxHealth;
    public float MovementSpeed => movementSpeed;
    public int JumpHeight => jumpHeight;
    public int AttackDamage => attackDamage;
    public float  AttackCooldown => attackCooldown;
    public float AttackRadius => attackRadius;
}
