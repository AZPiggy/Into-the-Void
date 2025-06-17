using UnityEngine;

public class BearTurn : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            // Ask the parent to flip
            BearEnemy bear = GetComponentInParent<BearEnemy>(); // get the script
            if (bear != null)
            {
                bear.FlipDirection();
                return;
            }
        }
    }
}
