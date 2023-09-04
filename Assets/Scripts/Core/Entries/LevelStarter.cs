using Core.Differs;
using Core.Guesses;
using Core.Timer;
using Core.UI;
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
        private DiffersInstaller _currentLevel;
        private bool _isLevelStarted;

        private void Awake()
        {
            _eventBus = EventBus.EventBus.Instance;
        }

        public void StartLevel(DiffersInstaller differsInstaller)
        {
            _currentLevel = differsInstaller;
            _isLevelStarted = true;
            
            _eventBus.TriggerEvent(EventName.ON_AD_OPEN);
            _eventBus.AddListener(EventName.ON_AD_WATCHED, SpawnLevel);

            SpawnLevel();
            _eventBus.RemoveListener(EventName.ON_AD_WATCHED, SpawnLevel);
        }

        public void EndLevel()
        {
            if (!_isLevelStarted) return;
            
            _eventBus.RemoveListener(EventName.ON_AD_WATCHED, SpawnLevel);
            
            DestroyLevel();
        }

        private void SpawnLevel()
        {
            _installer = Instantiate(_currentLevel, _differsPlaceholder);
            
            _installer.Construct();
            _guessesInstaller.Construct(_installer.TotalDiffers);
            _timer.Construct();
        }

        private void DestroyLevel()
        {
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