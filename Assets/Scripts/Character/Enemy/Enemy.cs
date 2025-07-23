using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Rigidbody2D rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public EnemyAnimation enemyAnimation { get; private set; }
    public Health health { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        health = GetComponent<Health>();
    }
}
