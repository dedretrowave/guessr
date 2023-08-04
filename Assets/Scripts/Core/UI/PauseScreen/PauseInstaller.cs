using Core.UI.PauseScreen.View;
using EventBus;
using UnityEngine;

namespace Core.UI.PauseScreen
{
    public class PauseInstaller : MonoBehaviour
    {
        [SerializeField] private PauseView _view;

        private EventBus.EventBus _eventBus;

        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;

            _view.OnClose += Unpause;
            _view.OnOpen += Pause;
            _view.OnBackToMenu += OnBackToMenu;
        }

        public void Disable()
        {
            _view.OnClose -= Unpause;
            _view.OnOpen -= Pause;
            _view.OnBackToMenu -= OnBackToMenu;
        }

        private void Pause()
        {
            _eventBus.TriggerEvent(EventName.ON_PAUSE);
        }

        private void Unpause()
        {
            _eventBus.TriggerEvent(EventName.ON_UNPAUSE);
        }

        private void OnBackToMenu()
        {
            _eventBus.TriggerEvent(EventName.ON_BACK_TO_MENU);
        }
    }
}