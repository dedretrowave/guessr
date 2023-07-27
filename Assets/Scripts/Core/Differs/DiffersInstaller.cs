using Core.Differs.Components;
using Core.Differs.Model;
using Core.Differs.Presenter;
using Core.Differs.View;
using EventBus;
using UnityEngine;

namespace Core.Differs
{
    public class DiffersInstaller : MonoBehaviour
    {
        [SerializeField] private DiffersView _view;

        private EventBus.EventBus _eventBus;

        private DiffersPresenter _presenter;

        private DiffersModel _model;
        
        public int TotalDiffers { get; private set; }

        public void Construct()
        {
            TotalDiffers = GetComponentInChildren<DiffersFrame>().CorrectPanelsCount;
            
            _eventBus = EventBus.EventBus.Instance;

            _model = new(TotalDiffers);
            
            _presenter = new(_view, _model);

            _view.OnCorrectDifferClicked += _presenter.OnDifferFound;
            _view.OnWrongPanelClicked += OnWrongClicked;
            _presenter.OnFound += OnFound;
            _presenter.OnAllDiffersFound += OnAllFound;
            
            _eventBus.AddListener(EventName.ON_HINT, _presenter.OnRandomDifferFound);
        }
        
        public void Disable()
        {
            if (_view == null) return;
            
            _view.OnCorrectDifferClicked -= _presenter.OnDifferFound;
            _view.OnWrongPanelClicked -= OnWrongClicked;
            _presenter.OnFound -= OnFound;
            _presenter.OnAllDiffersFound -= OnAllFound;
            
            _eventBus.RemoveListener(EventName.ON_HINT, _presenter.OnRandomDifferFound);
        }

        private void OnWrongClicked()
        {
            _eventBus.TriggerEvent(EventName.ON_WRONG_DIFFER_FOUND);
        }

        private void OnFound()
        {
            _eventBus.TriggerEvent(EventName.ON_DIFFER_FOUND);
        }

        private void OnAllFound()
        {
            _eventBus.TriggerEvent(EventName.ON_ALL_DIFFERS_FOUND);
        }
    }
}