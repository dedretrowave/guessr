using System;
using Core.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Differs.Components
{
    public class WrongPanel : MonoBehaviour, IPointerDownHandler
    {
        public Action OnClicked;

        private WrongPopup _wrongPopup;

        private void Awake()
        {
            _wrongPopup = Resources.Load<WrongPopup>("Prefabs/WrongPopup");
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            WrongPopup popup = Instantiate(_wrongPopup,
                eventData.position,
                Quaternion.identity,
                transform.parent);
            OnClicked?.Invoke();
        }
    }
}