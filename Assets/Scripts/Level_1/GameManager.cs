using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public enum GameState { None, StartMenu, GetReady, Playing, GameOver }

public class GameManager : MonoBehaviour
{
    public GameState currentState = GameState.None;

    public int score = 0;

    public int livesRemaining = 0;
    private int LIVES_AT_START = 3;

    public GameObject enemiesPrefab;
    private GameObject currentEnemies;

    public GameObject playerPrefab;
    private GameObject currentPlayer;

    public GameObject coinsPrefab;
    private GameObject currentCoins;

    public GameObject platformPrefab;
    private GameObject currentPlatforms;

    public TextMeshProUGUI messageOverlay;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Life;

    public TextMeshProUGUI countdownText;
    public float levelDuration = 180f;
    private float remainingTime;
    private bool timerRunning = false;

    public static GameManager game;

    // game camera
    public SmoothCameraFollow cameraScript;

    private void Awake()
    {
        if (game)
        {
            Destroy(this.gameObject);
        }
        else
        {
            game = this;
            // Keep score and lives
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        currentState = GameState.StartMenu;
        messageOverlay.text = "Forest Adventure\n\nPress \"S\" to Start";
        Score.enabled = false;
        Life.enabled = false;
        countdownText.enabled = false;
    }

    void Update()
    {
        if (currentState == GameState.StartMenu)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartNewGame();
            }
        }
        else if (currentState == GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
        // set timer
        if (currentState == GameState.Playing && timerRunning)
        {
            remainingTime -= Time.deltaTime;
            // clamp remaining time
            remainingTime = Mathf.Max(remainingTime, 0f);

            int seconds = Mathf.FloorToInt(remainingTime);
            countdownText.text = seconds + "s";

            if (remainingTime <= 0f)
            {
                timerRunning = false;
                LoseGameOver(); // time ran out
            }
        }
    }

    private void RestartGame()
    {
        messageOverlay.enabled = false;
        countdownText.enabled = false;
        StartNewGame();
    }

    private void StartNewGame()
    {
        score = 0;
        livesRemaining = LIVES_AT_START;

        if (currentEnemies)
        {
            Destroy(currentEnemies);
        }
        currentEnemies = Instantiate(enemiesPrefab);

        if (currentPlayer)
        {
            Destroy(currentPlayer);
        }
        currentPlayer = Instantiate(playerPrefab);

        // link camera to new player
        if (cameraScript != null)
        {
            cameraScript.playerObject = currentPlayer;

            // force camera to snap to player X
            Vector3 camPos = cameraScript.transform.position;
            camPos.x = currentPlayer.transform.position.x;
            cameraScript.transform.position = camPos;
        }

        if (currentCoins)
        {
            Destroy(currentCoins);
        }
        currentCoins = Instantiate(coinsPrefab);

        if (currentPlatforms)
        {
            Destroy(currentPlatforms);
        }
        currentPlatforms = Instantiate(platformPrefab);

        StartCoroutine(GetReady());
    }


    private IEnumerator GetReady()
    {
        currentState = GameState.GetReady;

        SoundManager.S.startTheMusic();

        messageOverlay.text = "Get Ready!";
        messageOverlay.enabled = true;

        Score.enabled = false;
        Life.enabled = false;

        yield return new WaitForSeconds(2f);

        for (int i = 3; i > 0; i--)
        {
            messageOverlay.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        messageOverlay.text = "Go!";
        yield return new WaitForSeconds(1f);

        messageOverlay.enabled = false;

        StartRound();
    }

    private void StartRound()
    {
        currentState = GameState.Playing;
        UpdateUI();

        // timer prep
        remainingTime = levelDuration;
        timerRunning = true;
        countdownText.enabled = true;
    }

    public void PlayerTakesDamage()
    {
        if (currentState != GameState.Playing) return;

        livesRemaining--;
        UpdateUI();

        if (livesRemaining <= 0)
        {
            CharacterController2D playerScript = currentPlayer.GetComponent<CharacterController2D>();
            if (playerScript != null)
            {
                playerScript.PlayDeathAnimation();
            }
            StartCoroutine(DeathSequence());
        }
    }

    public void PlayerInstantDeath()
    {
        if (currentState != GameState.Playing) return;

        livesRemaining = 0;
        UpdateUI();

        CharacterController2D playerScript = currentPlayer.GetComponent<CharacterController2D>();
        if (playerScript != null)
        {
            playerScript.PlayDeathAnimation();
        }

        // delay game over: want the death anim to finish
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(1.2f); // match anim length
        LoseGameOver();
    }

    public void UpdateUI()
    {
        if (currentState == GameState.Playing || currentState == GameState.GameOver)
        {
            Score.enabled = true;
            Life.enabled = true;
            Score.text = "Score: " + score;
            Life.text = "Lives: " + livesRemaining;
        }
        else
        {
            Score.enabled = false;
            Life.enabled = false;
        }
    }

    private void LoseGameOver()
    {
        currentState = GameState.GameOver;
        // stop timer
        timerRunning = false;
        countdownText.enabled = false;

        messageOverlay.text = "You lose :(\n\nPress \"R\" to Restart";
        messageOverlay.enabled = true;

        SoundManager.S.stopTheMusic();
        SoundManager.S.PlayLoseSound();

        // TODO: wait and then restart level
        // LevelManager.level.LevelEvent_RESTART_LEVEL();
    }

    public IEnumerator CompleteLevel()
    {
        currentState = GameState.GameOver;
        // stop timer
        timerRunning = false;
        countdownText.enabled = false;

        // stop music and freeze player
        SoundManager.S.stopTheMusic();
        SoundManager.S.PlayVictorySound();

        // UI
        messageOverlay.text = "Level Complete! Get Ready for next!";
        messageOverlay.enabled = true;
        UpdateUI();

        Animator playerAnim = currentPlayer.GetComponentInChildren<Animator>();
        if (playerAnim != null)
        {
            // disable animation
            playerAnim.enabled = false;
        }

        yield return new WaitForSeconds(3f);

        LevelManager.level.LevelEvent_END_OF_LEVEL();

    }
}
