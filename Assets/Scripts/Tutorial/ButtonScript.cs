using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public void btn_StartTheGame()
    {
        LevelManager.level.LevelEvent_StartTheGame();
    }

    public void btn_ReturnToMenu()
    {
        LevelManager.level.LevelEvent_GoToMenuScreen();
    }

    public void btn_Tutorial()
    {
        LevelManager.level.LevelEvent_GoToTutorial();
    }

    public void btn_Credits()
    {
        LevelManager.level.LevelEvent_GoToCredits();
    }

    public void btn_Quit()
    {
        Application.Quit();
    }

    public void btn_RestartLevel()
    {
        LevelManager.level.LevelEvent_RESTART_LEVEL();
    }

    public void btn_NextLevel()
    {
        LevelManager.level.LevelEvent_END_OF_LEVEL();
    }

}
