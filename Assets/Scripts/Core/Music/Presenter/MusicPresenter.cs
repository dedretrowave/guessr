using Core.Music.Model;
using Core.Music.View;
using UnityEngine;

namespace Core.Music.Presenter
{
    public class MusicPresenter
    {
        private MusicModel _model;

        private MusicView _view;

        public bool IsEnabled => _model.IsEnabled;

        public MusicPresenter(MusicView view, AudioClip music)
        {
            _model = new();
            
            _view = view;
            
            _view.SetEnabled(_model.IsEnabled);
            _view.SetTrack(music);
        }

        public void Mute()
        {
            if (_model.IsEnabled)
            {
                _view.SetEnabled(false);
            }
        }

        public void Unmute()
        {
            if (_model.IsEnabled)
            {
                _view.SetEnabled(true);
            }
        }

        public void SetEnabled(bool value)
        {
            _model.SetEnabled(value);
            _view.SetEnabled(_model.IsEnabled);
        }
    }
}