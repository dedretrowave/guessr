using System.Collections;
using Core.Timer.Presenter;
using Core.Timer.View;
using EventBus;
using UnityEngine;

namespace Core.Timer
{
    public class TimerInstaller : MonoBehaviour
    {
        [SerializeField] private TimerView _view;
        [SerializeField] private int _initialTimeInSecs;
        [SerializeField] private int _onWrongTimeLoss;

        private EventBus.EventBus _eventBus;
        
        private TimerPresenter _presenter;

        private Coroutine _timeout;

        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;
            
            _presenter = new(_view, _initialTimeInSecs);

            _presenter.OnTimeout += OnTimeout;
            _eventBus.AddListener(EventName.ON_WRONG_DIFFER_FOUND, OnWrongDifferFound);
            _eventBus.AddListener(EventName.ON_PAUSE, StopCountdown);
            _eventBus.AddListener(EventName.ON_UNPAUSE, StartCountdown);

            StartCountdown();
        }

        public void Disable()
        {
            _presenter.OnTimeout -= OnTimeout;
            _eventBus.RemoveListener(EventName.ON_WRONG_DIFFER_FOUND, OnWrongDifferFound);
            _eventBus.RemoveListener(EventName.ON_PAUSE, StopCountdown);
            _eventBus.RemoveListener(EventName.ON_UNPAUSE, StartCountdown);
            
            StopCountdown();
        }

        private void OnTimeout()
        {
            StopCountdown();
            _eventBus.TriggerEvent(EventName.ON_TIMEOUT);
        }

        private void OnWrongDifferFound()
        {
            _presenter.Decrease(_onWrongTimeLoss);
        }

        private void StartCountdown()
        {
            _timeout = StartCoroutine(Countdown());
        }
        
        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(1);
            _presenter.DecreaseByOneSec();
            yield return Countdown();
        }

        private void StopCountdown()
        {
            if (_timeout == null) return;
            
            StopCoroutine(_timeout);
            _timeout = null;
        }
    }
}