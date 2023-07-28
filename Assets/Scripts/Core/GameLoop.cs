using System;
using Core.Differs;
using Core.Entries;
using Core.Levels;
using Core.Music;
using Core.Score;
using Core.UI.LoseScreen;
using Core.UI.PauseScreen;
using Core.UI.WinScreen;
using EventBus;
using Save;
using UnityEngine;

namespace Core
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Ads.Ads _ads;
        [SerializeField] private LevelStarter _levelStarter;
        [SerializeField] private WinScreenInstaller _winScreen;
        [SerializeField] private LoseScreenInstaller _loseScreen;
        [SerializeField] private PauseInstaller _pause;
        [SerializeField] private ScoreInstaller _score;
        [SerializeField] private MusicInstaller _music;

        private LevelsContainer _levelsContainer;

        private EventBus.EventBus _eventBus;

        private bool _allLevelsPassed;

        private void Start()
        {
            _eventBus = EventBus.EventBus.Instance;

            _score.Construct();
            _winScreen.Construct();
            _loseScreen.Construct();
            _pause.Construct();
            _music.Construct();
            
            _eventBus.AddListener(EventName.ON_AD_OPEN, _ads.ShowAd);
            _eventBus.AddListener(EventName.ON_REWARDED_OPEN, _ads.ShowRewarded);

            _levelsContainer = new(_score.Score);
            _levelStarter.StartLevel(_levelsContainer.GetCurrent());

            _eventBus.AddListener(EventName.ON_ALL_DIFFERS_FOUND, OnComplete);
            _eventBus.AddListener(EventName.ON_REPLAY, OnReplay);
            _eventBus.AddListener(EventName.ON_TIMEOUT, OnLose);
        }

        private void OnDisable()
        {
            _score.Disable();
            _winScreen.Disable();
            _loseScreen.Disable();
            _pause.Disable();
            _levelStarter.EndLevel();
            
            _eventBus.RemoveListener(EventName.ON_AD_OPEN, _ads.ShowAd);
            _eventBus.RemoveListener(EventName.ON_REWARDED_OPEN, _ads.ShowRewarded);
            
            _eventBus.RemoveListener(EventName.ON_ALL_DIFFERS_FOUND, OnComplete);
            _eventBus.RemoveListener(EventName.ON_REPLAY, OnReplay);
            _eventBus.RemoveListener(EventName.ON_TIMEOUT, OnLose);
        }

        private void OnLose()
        {
            _levelStarter.EndLevel();
        }

        private void OnReplay()
        {
            _levelStarter.EndLevel();
            _levelStarter.StartLevel(_levelsContainer.GetCurrent());
        }

        private void OnComplete()
        {
            _levelStarter.EndLevel();

            DiffersInstaller nextLevel = _allLevelsPassed ?
                _levelsContainer.GetRandom() 
                : _levelsContainer.GetNext();

            if (nextLevel == null)
            {
                _eventBus.TriggerEvent(EventName.ON_ALL_LEVELS_PASSED);
                _allLevelsPassed = true;
                nextLevel = _levelsContainer.GetRandom();
            }
            
            _levelStarter.StartLevel(nextLevel);
        }
    }
}