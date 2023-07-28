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

        public LevelsContainer(int startingIndex)
        {
            _save = new();
            _levels = new(Resources.LoadAll<DiffersInstaller>(Path));

            _data = _save.Load<LevelsData>(LoadPath);

            if (_data == null)
            {
                _data = new(_levels.Count);
                _save.Save(LoadPath, _data);
                _currentLevelIndex = 0;
                return;
            }

            if (_data.NumberOfLevels < _levels.Count)
            {
                _data = new(_levels.Count);
                _save.Save(LoadPath, _data);
                _currentLevelIndex = _levels.Count - 1;
                return;
            }

            if (startingIndex >= _levels.Count)
            {
                _currentLevelIndex = Random.Range(0, _levels.Count);
                return;
            }
            
            _currentLevelIndex = startingIndex;
        }

        public DiffersInstaller GetNext()
        {
            _currentLevelIndex++;
            
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
                return null;
            }
        }

        public DiffersInstaller GetRandom()
        {
            int index = Random.Range(0, _levels.Count);
            _currentLevelIndex = index;
            return GetCurrent();
        }
    }

    [Serializable]
    internal class LevelsData
    {
        private int _numberOfLevels;
        public int NumberOfLevels => _numberOfLevels;

        [JsonConstructor]
        public LevelsData(int numberOfLevels)
        {
            if (_numberOfLevels < 0) return;
            
            _numberOfLevels = numberOfLevels;
        }
    }
}