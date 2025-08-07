using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsSounds : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Managers.AudioManager.PlayPressSound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Managers.AudioManager.PlayHoverSound();
    }
}
