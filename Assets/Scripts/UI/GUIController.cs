using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance {  get; private set; }

    [SerializeField] private GameObject _resultWindow;
    [SerializeField] private TMP_Text _resultText;

    [SerializeField] private Button _continueButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _resultWindow.SetActive(false);
    }

    private void OnEnable()
    {
        Managers.ScenesMananger.OnVictory += OnVictory;
        Managers.ScenesMananger.OnDefeat += OnDefeat;
    }

    private void OnDisable()
    {
        Managers.ScenesMananger.OnVictory -= OnVictory;
        Managers.ScenesMananger.OnDefeat -= OnDefeat;
    }
    public void ResetTime()
    {
        Managers.ScenesMananger.ResetTime();
    }

    public void PauseButton()
    {
        Managers.ScenesMananger.PauseTime();
    }

    public void ContinueButton()
    {
        Managers.ScenesMananger.ResetTime();
    }

    public void RestartButton()
    {
        Managers.ScenesMananger.RestartLevel();
        Managers.ScenesMananger.ResetTime();
    }

    public void MainMenuButton()
    {
        Managers.ScenesMananger.MainMenu();
        Managers.ScenesMananger.ResetTime();
    }

    public void RestartLevelFromCheckpoint()
    {
        Managers.ScenesMananger.RestartFromCheckpoint();
    }

    private void OnVictory()
    {
        _resultText.text = "Victory!";
        _continueButton.gameObject.SetActive(false);
        _resultWindow.SetActive(true);
    }

    private void OnDefeat()
    {
        _resultText.text = "Defeat!";
        _resultWindow.SetActive(true);
    }
}
