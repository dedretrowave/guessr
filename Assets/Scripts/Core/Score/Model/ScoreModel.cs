using System;
using Newtonsoft.Json;
using Save;
using UnityEngine;

namespace Core.Score.Model
{
    [Serializable]
    public class ScoreModel
    {
        private const string LoadPath = "score";
        private SaveFileHandler _save;
        
        private int _score;

        public int Score => _score;

        public ScoreModel()
        {
            _save = new();

            ScoreModel model = _save.Load<ScoreModel>(LoadPath);
            
            _score = model?.Score ?? 0;
        }

        [JsonConstructor]
        public ScoreModel(int score)
        {
            _score = score;
        }

        public void MarkAllLevelsPassed()
        {
            _save.Save(LoadPath, this);
        }

        public void MarkAllLevelsNotPassed()
        {
            _save.Save(LoadPath, this);
        }

        public void IncreaseScore()
        {
            _score++;
            _save.Save(LoadPath, this);
        }
    }
}