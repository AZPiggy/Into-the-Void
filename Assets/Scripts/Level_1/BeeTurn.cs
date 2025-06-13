using UnityEngine;

public class BeeTurn : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            // Ask the parent to flip
            BeeEnemy bee = GetComponentInParent<BeeEnemy>(); // get the script
            if (bee != null)
            {
                bee.FlipDirection();
                return;
            }
        }
    }
}

