using System;
using Core.Score;
using Core.UI.WinScreen.View;
using EventBus;
using UnityEngine;

namespace Core.UI.WinScreen
{
    public class WinScreenInstaller : MonoBehaviour
    {
        [SerializeField] private WinScreenView _view;
        
        [SerializeField] private ScoreInstaller _score;

        private EventBus.EventBus _eventBus;

        public void Construct()
        {
            _score.Construct();

            _view.Hide();

            _eventBus = EventBus.EventBus.Instance;
            
            _eventBus.AddListener(EventName.ON_ALL_DIFFERS_FOUND, _view.Show);
            _eventBus.AddListener(EventName.ON_ALL_LEVELS_PASSED, _view.ShowAllLevelsPassed);

            _view.OnBack += OnBack;
            _view.OnNext += OnNext;
        }

        public void Disable()
        {
            _eventBus.RemoveListener(EventName.ON_ALL_DIFFERS_FOUND, _view.Show);
            _eventBus.RemoveListener(EventName.ON_ALL_LEVELS_PASSED, _view.ShowAllLevelsPassed);
            
            _view.OnBack -= OnBack;
            _view.OnNext -= OnNext;
            
            _score.Disable();
        }

        private void OnBack()
        {
            _eventBus.TriggerEvent(EventName.ON_BACK_TO_MENU);
            _view.Hide();
        }

        private void OnNext()
        {
            _eventBus.TriggerEvent(EventName.ON_NEXT_LEVEL);
            _view.Hide();
        }
    }
}