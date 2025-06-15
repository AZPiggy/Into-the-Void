using UnityEngine;

public class GameManagerDestroy : MonoBehaviour
{
    private void Start()
    {
        // runs in menu screen. If the game manager is still alive, destroy it (shouldn't be here at this moment)
        if (GameManager.game) { Destroy(GameManager.game.gameObject); }
    }
}
