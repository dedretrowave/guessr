using Core.Score.Presenter;
using Core.Score.View;
using DI;
using EventBus;
using UnityEngine;

namespace Core.Score
{
    public class ScoreInstaller : MonoBehaviour
    {
        [SerializeField] private ScoreView _view;

        private EventBus.EventBus _eventBus;

        private ScorePresenter _presenter;

        public int Score => _presenter.Score;

        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;
            
            _presenter = new(_view);
            
            DependencyContext.Dependencies.Add(new(typeof(ScorePresenter), () => _presenter));
            
            _eventBus.AddListener(EventName.ON_ALL_DIFFERS_FOUND, _presenter.IncreaseScore);
        }

        public void Disable()
        {
            _eventBus.RemoveListener(EventName.ON_ALL_DIFFERS_FOUND, _presenter.IncreaseScore);
        }
    }
}