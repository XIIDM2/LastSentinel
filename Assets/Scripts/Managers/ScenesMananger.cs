using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesMananger : MonoBehaviour, IGameManager
{
    public void PauseTime()
    {
        Time.timeScale = 0.0f;
    }

    public void ResetTime()
    {
        Time.timeScale = 1.0f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
