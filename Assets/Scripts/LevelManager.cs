using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager level;

    private void Awake()
    {
        if (level)
        {
            Destroy(this.gameObject);
        }
        else
        {
            level = this;
        }
    }
    public void LevelEvent_StartTheGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void LevelEvent_GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LevelEvent_GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LevelEvent_GoToMenuScreen()
    {
        SceneManager.LoadScene("Menu_Scene");
    }

    public void LevelEvent_END_OF_LEVEL()
    {
        // get the current scene
        Scene thisScene = SceneManager.GetActiveScene();
        int thisIndex = thisScene.buildIndex;

        SceneManager.LoadScene(thisIndex + 1);
    }

    public void LevelEvent_RESTART_LEVEL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
