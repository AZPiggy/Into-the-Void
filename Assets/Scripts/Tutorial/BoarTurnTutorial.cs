using UnityEngine;

public class BoarTurnTutorial : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            // Ask the parent to flip
            BoarTutorialScript boar = GetComponentInParent<BoarTutorialScript>(); // get the script
            if (boar != null)
            {
                boar.FlipDirection();
                return;
            }
        }
    }
}
