using UnityEngine;

public class BatTurn : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            // Ask the parent to flip
            BatEnemy bat = GetComponentInParent<BatEnemy>(); // get the script
            if (bat != null)
            {
                bat.FlipDirection();
                return;
            }
        }
    }
}