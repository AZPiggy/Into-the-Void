using UnityEngine;

public class BoarEnemy : MonoBehaviour
{
    public float enemySpeed = 1f;
    public bool faceLeft = true;

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
            SoundManager.S.PlayPlayerDestroySound();
            GameManager.game.PlayerInstantDeath();
            Destroy(gameObject);
        }
    }
}


