using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && (GameManager.game.currentState == GameState.Playing))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && (GameManager.game.currentState == GameState.Playing))
        {
            collision.transform.parent = null;
        }
    }



}