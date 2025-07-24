using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharactedData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int jumpForce;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRadius;

    public int MaxHealth => maxHealth;
    public float MovementSpeed => movementSpeed;
    public int JumpForce => jumpForce;
    public int AttackDamage => attackDamage;
    public float AttackRadius => attackRadius;
}
