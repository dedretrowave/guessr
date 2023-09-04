using System;
using System.Collections.Generic;
using Core.Differs;
using EventBus;
using Newtonsoft.Json;
using Save;
using UnityEngine;

namespace Core.Levels
{
    public class LevelsContainer
    {
        private const string LoadPath = "levels";
        private const string Path = "Levels";
        private List<DiffersInstaller> _levels;

        private SaveFileHandler _save;
        private int _currentLevelIndex;
        private LevelsData _data;
        private EventBus.EventBus _eventBus;

        public LevelsContainer()
        {
            _eventBus = EventBus.EventBus.Instance;
            _save = new();
            _levels = new(Resources.LoadAll<DiffersInstaller>(Path));

            _data = _save.Load<LevelsData>(LoadPath);

            if (_data == null)
            {
                _data = new(_levels.Count, 0);
                _save.Save(LoadPath, _data);
                _currentLevelIndex = 0;
                return;
            }

            if (_data.NumberOfLevels < _levels.Count)
            {
                _data = new(_levels.Count, _data.LastPassedLevel);
                _save.Save(LoadPath, _data);
                _currentLevelIndex = _data.LastPassedLevel;
                return;
            }

            if (_data.LastPassedLevel == _levels.Count)
            {
                
            }
            
            _currentLevelIndex = _data.LastPassedLevel;
            _eventBus.AddListener(EventName.ON_ALL_DIFFERS_FOUND, IncreasePassedLevelsCount);
        }

        public void Disable()
        {
            _save.Save(LoadPath, _data);
            _eventBus.RemoveListener(EventName.ON_ALL_DIFFERS_FOUND, IncreasePassedLevelsCount);
        }

        private void IncreasePassedLevelsCount()
        {
            _data.LastPassedLevel++;
            _save.Save(LoadPath, _data);

            if (_data.LastPassedLevel == _levels.Count)
            {
                _eventBus.TriggerEvent(EventName.ON_ALL_LEVELS_PASSED);
            }
        }

        public DiffersInstaller GetNext()
        {
            _currentLevelIndex++;

            return GetCurrent();
        }

        public DiffersInstaller GetCurrent()
        {
            if (_currentLevelIndex >= _levels.Count)
            {
                _eventBus.TriggerEvent(EventName.ON_ALL_LEVELS_PASSED);
                return null;
            }
            
            try
            {
                return _levels[_currentLevelIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }
    }

    [Serializable]
    internal class LevelsData
    {
        private int _numberOfLevels;
        private int _lastPassedLevel;
        public int NumberOfLevels => _numberOfLevels;
        public int LastPassedLevel
        {
            get => _lastPassedLevel;
            set
            {
                if (value >= _numberOfLevels)
                {
                    _lastPassedLevel = _numberOfLevels;
                    return;
                }

                _lastPassedLevel = value;
            }
        }

        [JsonConstructor]
        public LevelsData(int numberOfLevels, int lastPassedLevel)
        {
            if (_numberOfLevels < 0) return;
            
            _numberOfLevels = numberOfLevels;
            _lastPassedLevel = lastPassedLevel;
        }
    }
}