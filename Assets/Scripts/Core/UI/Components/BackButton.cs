using System;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Components
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private EventBus.EventBus _eventBus;

        private void Awake()
        {
            _eventBus = EventBus.EventBus.Instance;
            
            _button.onClick.AddListener(OnPress);
        }

        private void OnPress()
        {
            _eventBus.TriggerEvent(EventName.ON_BACK_TO_MENU);
        }
    }
}