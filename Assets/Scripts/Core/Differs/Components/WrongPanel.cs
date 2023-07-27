using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Differs.Components
{
    public class WrongPanel : MonoBehaviour, IPointerDownHandler
    {
        public Action OnClicked;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }
    }
}