using Core.UI.LoseScreen.View;
using EventBus;
using UnityEngine;

namespace Core.UI.LoseScreen
{
    public class LoseScreenInstaller : MonoBehaviour
    {
        [SerializeField] private LoseScreenView _view;

        private EventBus.EventBus _eventBus;

        public void Construct()
        {
            _view.Hide();
            
            _eventBus = EventBus.EventBus.Instance;
            
            _eventBus.AddListener(EventName.ON_TIMEOUT, OnLose);

            _view.OnBack += OnBack;
            _view.OnReplay += OnReplay;
            _view.OnMoreTime += OnMoreTime;
        }

        public void Disable()
        {
            _eventBus.RemoveListener(EventName.ON_TIMEOUT, OnLose);
            
            _view.OnBack -= OnBack;
            _view.OnReplay -= OnReplay;
            _view.OnMoreTime -= OnMoreTime;
        }

        private void OnLose()
        {
            _view.Show();
        }

        private void OnReplay()
        {
            _eventBus.TriggerEvent(EventName.ON_REPLAY);
            _view.Hide();
        }

        private void OnBack()
        {
            _eventBus.TriggerEvent(EventName.ON_BACK_TO_MENU);
            _view.Hide();
        }

        private void OnMoreTime()
        {
            _eventBus.AddListener(EventName.ON_REWARDED_WATCHED, OnMoreTimeApplied);
            _eventBus.TriggerEvent(EventName.ON_REWARDED_OPEN);
            _view.Hide();
        }

        private void OnMoreTimeApplied()
        {
            _eventBus.RemoveListener(EventName.ON_REWARDED_WATCHED, OnMoreTimeApplied);
            _eventBus.TriggerEvent(EventName.ON_MORE_TIME);
        }
    }
}