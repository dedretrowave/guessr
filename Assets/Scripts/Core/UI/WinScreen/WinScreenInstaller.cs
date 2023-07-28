using Core.Score.Presenter;
using Core.UI.WinScreen.Presenter;
using Core.UI.WinScreen.View;
using DI;
using EventBus;
using UnityEngine;

namespace Core.UI.WinScreen
{
    public class WinScreenInstaller : MonoBehaviour
    {
        [SerializeField] private WinScreenView _view;

        private EventBus.EventBus _eventBus;

        private WinScreenPresenter _presenter;
        private ScorePresenter _scorePresenter;

        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;
            
            _presenter = new(_view);

            _scorePresenter = DependencyContext.Dependencies.Get<ScorePresenter>();
            
            // _eventBus.AddListener(EventName.ON_ALL_LEVELS_PASSED, OnAllLevelsPassed);
            _view.OnBack += OnBack;
        }

        public void Disable()
        {
            // _eventBus.RemoveListener(EventName.ON_ALL_LEVELS_PASSED, OnAllLevelsPassed);
            _view.OnBack -= OnBack;
        }

        private void OnAllLevelsPassed()
        {
            int totalScore = _scorePresenter.GetCurrentScore();
            _presenter.DisplayTotalScore(totalScore);
            _presenter.Show();
        }
        
        private void OnBack()
        {
            _eventBus.TriggerEvent(EventName.ON_BACK_TO_MENU);
        }
    }
}