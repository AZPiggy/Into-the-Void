using UnityEngine;

public class PlayerAnimScript : MonoBehaviour
{
    public void CallMeleeAttack()
    {
        transform.parent.BroadcastMessage("MeleeAttack");
    }
}