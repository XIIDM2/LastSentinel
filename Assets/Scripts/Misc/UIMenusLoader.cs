using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenusLoader : MonoBehaviour
{
    private const string PauseMenuName = "PauseMenu";
    private const string ResultMenuName = "ResultMenu";
    private const string MainMenuName = "MainMenu";

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != MainMenuName)
        {
            LoadPauseMenu();
            LoadResultMenu();
        }
        else
        {
            UnloadPauseMenu();
            UnloadResultMenu();
        }
    }

    private void LoadPauseMenu()
    {
        if (!SceneManager.GetSceneByName(PauseMenuName).isLoaded)
        {
            SceneManager.LoadSceneAsync(PauseMenuName, LoadSceneMode.Additive);
        }
    }

    private void LoadResultMenu()
    {
        if (!SceneManager.GetSceneByName(ResultMenuName).isLoaded)
        {
            SceneManager.LoadSceneAsync(ResultMenuName, LoadSceneMode.Additive);
        }
    }

    private void UnloadPauseMenu()
    {
        if (SceneManager.GetSceneByName(PauseMenuName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(PauseMenuName);
        }
    }

    private void UnloadResultMenu()
    {
        if (SceneManager.GetSceneByName(ResultMenuName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(ResultMenuName);
        }
    }
}
