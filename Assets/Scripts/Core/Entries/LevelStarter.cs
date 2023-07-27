using Core.Differs;
using Core.Guesses;
using Core.Timer;
using UnityEngine;

namespace Core.Entries
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private GuessesInstaller _guessesInstaller;
        [SerializeField] private TimerInstaller _timer;
        
        [SerializeField] private Transform _differsPlaceholder;

        private DiffersInstaller _installer;

        public void StartLevel(DiffersInstaller differsInstaller)
        {
            _installer = Instantiate(differsInstaller, _differsPlaceholder);
            
            _installer.Construct();
            _guessesInstaller.Construct(_installer.TotalDiffers);
            _timer.Construct();
        }

        public void EndLevel()
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