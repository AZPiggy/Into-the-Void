using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonScript : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound;
    public AudioSource UISoundManager;

    // if mouse hovering over our button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UISoundManager != null && hoverSound != null)
        {
            UISoundManager.PlayOneShot(hoverSound);
        }
    }
}