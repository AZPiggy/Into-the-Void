using UnityEngine;

public class CoinTutorialScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.S.PlayCoinSound();
            Destroy(gameObject);
        }
    }
}
