using UnityEngine;
using VContainer;


public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private Health health;

    private CharacterData characterData;

    [Inject]
    private void Construct(Health health, PlayerMovement playerMovement, PlayerAttack playerAttack, CharacterData characterData)
    {
       this.health = health;
       this.playerMovement = playerMovement;
       this.playerAttack = playerAttack;
       this.characterData = characterData;
    }

    private void Awake()
    {
        health.InitData(characterData);
        playerMovement.InitData(characterData);
        playerAttack.InitData(characterData);
    }

    private void Start()
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;
    }

    private void OnEnable()
    {
        health.Death += OnDeath;
    }
    private void OnDisable()
    {
        health.Death -= OnDeath;
    }

    private void OnDeath()
    {
        playerMovement.enabled = false;
        playerAttack.enabled = false;
    }

}


