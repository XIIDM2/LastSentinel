using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ResultWindowController : MonoBehaviour
{
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TMP_Text _resultText;

    private void Start()
    {
        _resultPanel.SetActive(false);
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

    public void ResetTime()
    {
        Managers.ScenesMananger.ResetTime();
    }

    public void RestartLevelFromCheckpoint()
    {
        Managers.ScenesMananger.RestartFromCheckpoint();
    }    

    private void OnVictory()
    {
        _resultText.text = "Victory!";
        _resultPanel.SetActive(true);
    }

    private void OnDefeat()
    {
        _resultText.text = "Defeat!";
        _resultPanel.SetActive(true);
    }
}
