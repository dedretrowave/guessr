using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.LoseScreen.View
{
    public class LoseScreenView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Button _back;
        [SerializeField] private Button _replay;
        [SerializeField] private Button _moreTime;
        
        private readonly int _isShown = Animator.StringToHash("IsShown");

        public Action OnBack;
        public Action OnReplay;
        public Action OnMoreTime;

        private void Awake()
        {
            _back.onClick.AddListener(OnBackPressed);
            _replay.onClick.AddListener(OnReplayPressed);
            _moreTime.onClick.AddListener(OnMoreTimePressed);
        }

        public void Show()
        {
            _animator.SetBool(_isShown, true);
        }

        public void Hide()
        {
            _animator.SetBool(_isShown, false);
        }

        public void OnBackPressed()
        {
            OnBack?.Invoke();
        }

        public void OnReplayPressed()
        {
            OnReplay?.Invoke();
        }

        public void OnMoreTimePressed()
        {
            OnMoreTime?.Invoke();
        }
    }
}