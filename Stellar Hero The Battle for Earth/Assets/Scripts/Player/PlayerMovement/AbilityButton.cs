using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IPointerClickHandler
{
    public Action Clicked;

    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke();
}
