using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        Managers.ScenesMananger.StartGameLevel();
    }

    public void QuitGame()
    {
        Managers.ScenesMananger.ExitGame();
    }
}
