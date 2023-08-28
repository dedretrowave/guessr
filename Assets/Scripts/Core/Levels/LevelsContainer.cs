using System;
using System.Collections.Generic;
using Core.Differs;
using Newtonsoft.Json;
using Save;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private bool _allLevelsPassed;

        public LevelsContainer()
        {
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
                _allLevelsPassed = true;
                _currentLevelIndex = Random.Range(0, _levels.Count);
                return;
            }
            
            _currentLevelIndex = _data.LastPassedLevel;
        }

        public void Disable()
        {
            _save.Save(LoadPath, _data);
        }

        public DiffersInstaller GetNext()
        {
            if (_allLevelsPassed)
            {
                return GetRandom();
            }
            
            _currentLevelIndex++;
            _data.LastPassedLevel++;

            return GetCurrent();
        }

        public DiffersInstaller GetCurrent()
        {
            try
            {
                return _levels[_currentLevelIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                _allLevelsPassed = true;
                return GetRandom();
            }
        }

        public DiffersInstaller GetRandom()
        {
            Debug.Log("RANDOM LEVEL!");
            int index = Random.Range(0, _levels.Count);
            _currentLevelIndex = index;
            return GetCurrent();
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
                Debug.Log(_lastPassedLevel);
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