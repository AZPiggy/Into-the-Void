using UnityEngine;

public class BoarEnemy : MonoBehaviour
{
    public float enemySpeed = 1f;
    public bool faceLeft = true;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private Collider2D col;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        // sprite.flipX = !faceLeft;
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

    public void BoarEnemyDie()
    {
        if (isDead) return;
        isDead = true;

        anim.SetTrigger("Die");
        SoundManager.S.PlayEnemyDestroySound();

        GameManager.game.score += 10;
        GameManager.game.UpdateUI();

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.enabled = false;

        Destroy(gameObject, 1.2f); // allow anim to finish
    }
}


