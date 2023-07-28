using Core.Differs;
using Core.Guesses;
using Core.Timer;
using EventBus;
using UnityEngine;

namespace Core.Entries
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private GuessesInstaller _guessesInstaller;
        [SerializeField] private TimerInstaller _timer;
        
        [SerializeField] private Transform _differsPlaceholder;

        private EventBus.EventBus _eventBus;

        private DiffersInstaller _installer;

        private int _levelIndex;

        public void StartLevel(DiffersInstaller differsInstaller)
        {
            _eventBus = EventBus.EventBus.Instance;
            
            _levelIndex++;
            _installer = Instantiate(differsInstaller, _differsPlaceholder);
            
            _installer.Construct();
            _guessesInstaller.Construct(_installer.TotalDiffers);
            _timer.Construct();
        }

        public void EndLevel()
        {
            if (_levelIndex == 2)
            {
                _levelIndex = 0;
                _eventBus.TriggerEvent(EventName.ON_AD_OPEN);
                _eventBus.AddListener(EventName.ON_AD_WATCHED, DestroyLevel);
                return;
            }
            
            DestroyLevel();
        }

        private void DestroyLevel()
        {
            _eventBus.RemoveListener(EventName.ON_AD_WATCHED, DestroyLevel);
            _guessesInstaller.Disable();
            _timer.Disable();
            _installer.Disable();

            if (_installer == null)
            {
                return;
            }
            
            Destroy(_installer.gameObject);
        }
    }
}