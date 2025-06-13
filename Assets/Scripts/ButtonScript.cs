using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private void Start()
    {
        // runs in menu screen. If the game manager is still alive, destroy it (shouldn't be here at this moment)
        if (GameManager.game) { Destroy(GameManager.game.gameObject); }
    }
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


}
