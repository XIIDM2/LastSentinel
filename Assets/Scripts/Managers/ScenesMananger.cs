using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesMananger : MonoBehaviour, IGameManager
{
    public event UnityAction OnVictory;
    public event UnityAction OnDefeat;
    public bool IsGameEnded => _isGameEnded;

    [SerializeField] private string _gameLevelName = "Level";

    private bool _isGameEnded = false;

    public void Victory()
    {
        OnVictory?.Invoke();
        _isGameEnded = true;
        PauseTime();
    }

    public void Defeat()
    {
        OnDefeat?.Invoke();
        _isGameEnded = true;
        PauseTime();
    }

    public void PauseTime()
    {
        Time.timeScale = 0.0f;
    }

    public void ResetTime()
    {
        Time.timeScale = 1.0f;
    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene(_gameLevelName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartFromCheckpoint()
    {
        CheckpointManager.Instance.SpawnPlayerAtPoint();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
