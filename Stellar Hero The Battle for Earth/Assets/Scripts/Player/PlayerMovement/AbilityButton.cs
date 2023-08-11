using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IPointerClickHandler
{
    public UnityAction Clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
    }

}
