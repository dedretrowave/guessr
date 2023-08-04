using Core.Music.Presenter;
using Core.Music.View;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Music
{
    public class MusicInstaller : MonoBehaviour
    {
        [SerializeField] private MusicView _view;
        [SerializeField] private AudioClip _track;
        [SerializeField] private Toggle _toggle;

        private EventBus.EventBus _eventBus;

        private MusicPresenter _presenter;

        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;
            
            _presenter = new(_view, _track);

            if (_toggle != null)
            {
                _toggle.isOn = _presenter.IsEnabled;
                
                _toggle.onValueChanged.AddListener(_presenter.SetEnabled);
            }
            
            _eventBus.AddListener(EventName.ON_REWARDED_OPEN, _presenter.Mute);
            _eventBus.AddListener(EventName.ON_AD_OPEN, _presenter.Mute);
            
            _eventBus.AddListener(EventName.ON_REWARDED_WATCHED, _presenter.Unmute);
            _eventBus.AddListener(EventName.ON_REWARDED_SKIPPED, _presenter.Unmute);
            _eventBus.AddListener(EventName.ON_AD_WATCHED, _presenter.Unmute);
        }

        public void Disable()
        {
            _eventBus.RemoveListener(EventName.ON_REWARDED_OPEN, _presenter.Mute);
            _eventBus.RemoveListener(EventName.ON_AD_OPEN, _presenter.Mute);
            
            _eventBus.RemoveListener(EventName.ON_REWARDED_WATCHED, _presenter.Unmute);
            _eventBus.RemoveListener(EventName.ON_REWARDED_SKIPPED, _presenter.Unmute);
            _eventBus.RemoveListener(EventName.ON_AD_WATCHED, _presenter.Unmute);
        }
    }
}