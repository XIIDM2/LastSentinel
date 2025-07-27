using UnityEngine;
using VContainer;


public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private Health health;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();

        health = GetComponent<Health>();
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


