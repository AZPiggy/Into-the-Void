using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Background Music")] // customize the editor

    [SerializeField] // can still access in the Unity editor
    private AudioSource backgroundMusic;
    [SerializeField]
    private float bgmStartTime = 0;

    [Header("Action Sounds")]
    public AudioSource soundEffects;

    public AudioClip enemyExplosionClip, playerExplosionClip;

    [Header("Victory Sound")]
    public AudioClip victoryClip;

    [Header("Lose Sound")]
    public AudioClip loseClip;

    [Header("Coin Sound")]
    public AudioSource coinSoundObject;
    public AudioClip lifeBonusSound;

    [Header("Player Actions")]
    public AudioClip jumpClip;
    public AudioClip attackClip;


    // Singleton Variable
    public static SoundManager S;

    // runs before Start
    private void Awake()
    {
        if (S)
        {
            Destroy(this.gameObject);
        }
        else
        {
            S = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startTheMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.time = bgmStartTime;
            backgroundMusic.Play();
        }
    }

    public void stopTheMusic()
    {
        backgroundMusic.Stop();
    }

    public void PlayVictorySound()
    {
        soundEffects.PlayOneShot(victoryClip);
    }

    public void PlayLoseSound()
    {
        soundEffects.PlayOneShot(loseClip);
    }

    public void PlayCoinSound()
    {
        coinSoundObject.Play();
    }

    public void PlayEnemyDestroySound()
    {
        soundEffects.PlayOneShot(enemyExplosionClip);
    }

    public void PlayPlayerDestroySound()
    {
        soundEffects.PlayOneShot(playerExplosionClip);
    }

    public void PlayJumpSound()
    {
        soundEffects.PlayOneShot(jumpClip);
    }

    public void PlayAttackSound()
    {
        soundEffects.PlayOneShot(attackClip);
    }

    public void PlayLifeBonusSound()
    {
        coinSoundObject.PlayOneShot(lifeBonusSound);
    }

}
