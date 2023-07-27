using Core.Music.Presenter;
using Core.Music.View;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Music
{
    public class MusicInstaller : MonoBehaviour
    {
        [SerializeField] private MusicView _view;
        [SerializeField] private AudioClip _track;
        [SerializeField] private Toggle _toggle;

        private MusicPresenter _presenter;

        public void Construct()
        {
            _presenter = new(_view, _track);

            if (_toggle != null)
            {
                _toggle.isOn = _presenter.IsEnabled;
                
                _toggle.onValueChanged.AddListener(_presenter.SetEnabled);
            }
        }
    }
}