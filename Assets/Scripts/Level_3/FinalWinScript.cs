using UnityEngine;
using System.Collections;

public class FinalWinScript : MonoBehaviour
{
    public GameObject finalWinPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalWinPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameManager.game.currentState == GameState.Playing)
        {
            StartCoroutine(CompleteGame());
        }
    }

    public IEnumerator CompleteGame()
    {
        GameManager.game.currentState = GameState.GameOver;
        // stop timer
        GameManager.game.timerRunning = false;
        GameManager.game.countdownText.enabled = false;

        // stop music and freeze player
        SoundManager.S.stopTheMusic();
        SoundManager.S.PlayVictorySound();

        // UI
        GameManager.game.UpdateUI();

        Animator playerAnim = GameManager.game.currentPlayer.GetComponentInChildren<Animator>();
        if (playerAnim != null)
        {
            // disable animation
            playerAnim.enabled = false;
        }

        yield return new WaitForSeconds(1f);

        // LevelManager.level.LevelEvent_END_OF_LEVEL();
        finalWinPanel.SetActive(true);

    }
}
