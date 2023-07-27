using System;
using System.Collections.Generic;
using Core.Differs;
using UnityEngine;

namespace Core.Levels
{
    public class LevelsContainer
    {
        private const string Path = "Levels";
        private List<DiffersInstaller> _levels;

        private int _currentLevelIndex;

        public LevelsContainer()
        {
            _levels = new(Resources.LoadAll<DiffersInstaller>(Path));
            _currentLevelIndex = 0;
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
    }
}