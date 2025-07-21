using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Rigidbody2D rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public PlayerInputController playerInputController {  get; private set; }
    public PlayerMovement playerMovement {  get; private set; }
    public PlayerAttack playerAttack {  get; private set; }
    public PlayerAnimation playerAnimation {  get; private set; }
    public Health Health {  get; private set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInputController = GetComponent<PlayerInputController>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerAnimation = GetComponent<PlayerAnimation>();
        Health = GetComponent<Health>();
    }
}
