using UnityEngine;

public class BoarTurn : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            // Ask the parent to flip
            BoarEnemy boar = GetComponentInParent<BoarEnemy>(); // get the script
            if (boar != null)
            {
                boar.FlipDirection();
                return;
            }
        }
    }
}

