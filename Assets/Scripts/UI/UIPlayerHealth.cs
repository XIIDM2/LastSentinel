using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _healthSlider;

    private Health _playerHealth;

    private void OnEnable()
    {
        if (CheckpointManager.Instance != null) CheckpointManager.Instance.OnPlayerSpawned += SetUIHealthStats;
        if (_playerHealth != null) _playerHealth.OnHealthChanged += UpdateUIHealthStats;
    }

    private void OnDisable()
    {
        if (CheckpointManager.Instance != null) CheckpointManager.Instance.OnPlayerSpawned -= SetUIHealthStats;
        if (_playerHealth != null) _playerHealth.OnHealthChanged -= UpdateUIHealthStats;
    }

    private void SetUIHealthStats(Health playerHealth)
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged -= UpdateUIHealthStats;
        }
        
        _playerHealth = playerHealth;

        _playerHealth.OnHealthChanged += UpdateUIHealthStats;
        UpdateUIHealthStats();
    }

    private void UpdateUIHealthStats()
    {
        _healthText.text = $"{_playerHealth.GetCurrentHealth()}/{_playerHealth.GetMaxHealth()}";
        _healthSlider.maxValue = _playerHealth.GetMaxHealth();
        _healthSlider.value = _playerHealth.GetCurrentHealth();
    } 
}
