using System;
using Core.Differs;
using Core.Entries;
using Core.Levels;
using Core.Music;
using Core.Score;
using Core.UI.LoseScreen;
using Core.UI.PauseScreen;
using Core.UI.WinScreen;
using DI;
using EventBus;
using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Ads.Ads _ads;
        [SerializeField] private LevelStarter _levelStarter;
        [SerializeField] private LoseScreenInstaller _loseScreen;
        [SerializeField] private WinScreenInstaller _winScreen;
        [SerializeField] private PauseInstaller _pause;
        [SerializeField] private ScoreInstaller _score;
        [SerializeField] private MusicInstaller _music;
        [SerializeField] private SaveMediator _saveMediator;

        private LevelsContainer _levelsContainer;

        private EventBus.EventBus _eventBus;

        private void Start()
        {
            _eventBus = EventBus.EventBus.Instance;

            DependencyContext.Dependencies.Add(new(typeof(SaveMediator), () => _saveMediator));

            _score.Construct();
            _loseScreen.Construct();
            _winScreen.Construct();
            _pause.Construct();
            _music.Construct();

            _eventBus.AddListener(EventName.ON_AD_OPEN, _ads.ShowAd);
            _eventBus.AddListener(EventName.ON_REWARDED_OPEN, _ads.ShowRewarded);

            _levelsContainer = new();
            StartLevel(_levelsContainer.GetCurrent());

            _eventBus.AddListener(EventName.ON_ALL_DIFFERS_FOUND, OnComplete);
            _eventBus.AddListener(EventName.ON_NEXT_LEVEL, StartNextLevel);
            
            _eventBus.AddListener(EventName.ON_REPLAY, OnReplay);
            _eventBus.AddListener(EventName.ON_BACK_TO_MENU, OnBackToMenu);
        }

        private void OnDisable()
        {
            _levelsContainer.Disable();
            _levelStarter.EndLevel();
            _score.Disable();
            _loseScreen.Disable();
            _winScreen.Disable();
            _pause.Disable();
            _music.Disable();

            _eventBus.RemoveListener(EventName.ON_AD_OPEN, _ads.ShowAd);
            _eventBus.RemoveListener(EventName.ON_REWARDED_OPEN, _ads.ShowRewarded);
            
            _eventBus.RemoveListener(EventName.ON_ALL_DIFFERS_FOUND, OnComplete);
            _eventBus.RemoveListener(EventName.ON_NEXT_LEVEL, StartNextLevel);
            _eventBus.RemoveListener(EventName.ON_REPLAY, OnReplay);
            _eventBus.RemoveListener(EventName.ON_BACK_TO_MENU, OnBackToMenu);
        }

        private void OnReplay()
        {
            _levelStarter.EndLevel();
            _levelStarter.StartLevel(_levelsContainer.GetCurrent());
        }

        private void OnComplete()
        {
            _levelStarter.EndLevel();
        }
        
        private void StartNextLevel()
        {
            DiffersInstaller nextLevel = _levelsContainer.GetNext();

            StartLevel(nextLevel);
        }

        private void StartLevel(DiffersInstaller level)
        {
            if (level == null) return;
            
            _levelStarter.StartLevel(level);
        }

        private void OnBackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}