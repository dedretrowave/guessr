using System;
using Newtonsoft.Json;
using Save;

namespace Core.Music.Model
{
    [Serializable]
    public class MusicModel
    {
        private const string LoadPath = "music";
        private SaveFileHandler _save;
        
        private bool _isEnabled;

        public bool IsEnabled => _isEnabled;

        [JsonConstructor]
        public MusicModel(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }

        public MusicModel()
        {
            _save = new();
            MusicModel data = _save.Load<MusicModel>(LoadPath);
            
            _isEnabled = data?.IsEnabled ?? true;
        }

        public void SetEnabled(bool value)
        {
            _isEnabled = value;
            _save.Save(LoadPath, this);
        }
    }
}