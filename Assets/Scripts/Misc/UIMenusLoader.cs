using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenusLoader : MonoBehaviour
{
    private const string GUISceneName = "GUI";
    private const string MainMenuName = "MainMenu";

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != MainMenuName)
        {
            LoadPauseMenu();
        }
        else
        {
            UnloadPauseMenu();
        }
    }

    private void LoadPauseMenu()
    {
        if (!SceneManager.GetSceneByName(GUISceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(GUISceneName, LoadSceneMode.Additive);
        }
    }

    private void UnloadPauseMenu()
    {
        if (SceneManager.GetSceneByName(GUISceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(GUISceneName);
        }
    }
}
