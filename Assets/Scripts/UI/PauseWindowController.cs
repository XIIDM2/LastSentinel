using UnityEngine;

public class PauseWindowController : MonoBehaviour
{
    public void PauseButton()
    {
        Managers.ScenesMananger.PauseTime();
    }

    public void RestartButton()
    {
        Managers.ScenesMananger.RestartLevel();
        Managers.ScenesMananger.ResetTime();
    }

    public void ContinueButton()
    {
        Managers.ScenesMananger.ResetTime();
    }

    public void MainMenuButton()
    {
        Managers.ScenesMananger.MainMenu();
        Managers.ScenesMananger.ResetTime();
    }
}
