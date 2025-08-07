using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _healthSlider;

    private Health _playerHealth;

    private void Awake()
    {
        _playerHealth = GameObject.FindFirstObjectByType<PlayerController>().GetComponent<Health>();
    }

    private void Start()
    {
        _healthSlider.maxValue = _playerHealth.GetMaxHealth();
        UpdateUIHealthStats();
    }

    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += UpdateUIHealthStats;
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= UpdateUIHealthStats;
    }

    private void UpdateUIHealthStats()
    {
        _healthText.text = $"{_playerHealth.GetCurrentHealth()}/{_playerHealth.GetMaxHealth()}";
        _healthSlider.value = _playerHealth.GetCurrentHealth();
    }
  
}
