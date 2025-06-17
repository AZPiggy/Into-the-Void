using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public float enemySpeed = 1f;
    public bool faceLeft = false;

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
        sprite.flipX = faceLeft ? true : false;
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

    public void BatEnemyDie()
    {
        if (isDead) return;
        isDead = true;

        anim.SetTrigger("Die");
        SoundManager.S.PlayEnemyDestroySound();

        GameManager.game.score += 2;
        GameManager.game.UpdateUI();

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.enabled = false;

        Destroy(gameObject, 1.2f); // allow anim to finish
    }
}


