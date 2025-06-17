using UnityEngine;

public class FinalWinScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameManager.game.currentState == GameState.Playing)
        {
            StartCoroutine(GameManager.game.CompleteGame());
        }
    }
}
