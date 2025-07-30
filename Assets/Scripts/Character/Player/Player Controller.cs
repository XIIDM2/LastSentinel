using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;

    private Health _health;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttack = GetComponent<PlayerAttack>();

        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _playerMovement.enabled = true;
        _playerAttack.enabled = true;
    }

    private void OnEnable()
    {
        _health.Death += OnDeath;
    }
    private void OnDisable()
    {
        _health.Death -= OnDeath;
    }

    private void OnDeath()
    {
        _playerMovement.enabled = false;
        _playerAttack.enabled = false;
    }

}


