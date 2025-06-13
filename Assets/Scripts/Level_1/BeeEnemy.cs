using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    public float enemySpeed = 1f;
    public bool faceLeft = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = !faceLeft;
    }

    void FixedUpdate()
    {
        float direction = faceLeft ? -1f : 1f;
        rb.linearVelocityX = enemySpeed * direction;
    }

    public void FlipDirection()
    {
        faceLeft = !faceLeft;
        sprite.flipX = faceLeft ? false : true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.S.PlayEnemyDestroySound();

            // flash to indicate hurt
            CharacterController2D playerScript = collision.gameObject.GetComponent<CharacterController2D>();
            if (playerScript != null)
            {
                playerScript.FlashRed();
            }

            GameManager.game.PlayerTakesDamage();
            Destroy(gameObject);
        }
    }
}


