using UnityEngine;

public class LifeBonusScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.game.livesRemaining++;
            GameManager.game.UpdateUI();

            SoundManager.S.PlayLifeBonusSound();
            Destroy(this.gameObject);
        }
    }
}
