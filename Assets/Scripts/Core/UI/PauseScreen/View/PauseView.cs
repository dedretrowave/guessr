using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.PauseScreen.View
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Button _open;
        [SerializeField] private Button _close;
        [SerializeField] private Button _backToMenu;
        
        private readonly int _isShown = Animator.StringToHash("IsShown");

        public Action OnOpen;
        public Action OnClose;
        public Action OnBackToMenu;

        private void Awake()
        {
            _open.onClick.AddListener(Show);
            _close.onClick.AddListener(Hide);
            _backToMenu.onClick.AddListener(BackToMenu);
        }

        private void Show()
        {
            _animator.SetBool(_isShown, true);
            OnOpen?.Invoke();
        }

        private void Hide()
        {
            _animator.SetBool(_isShown, false);
            OnClose?.Invoke();
        }

        private void BackToMenu()
        {
            OnBackToMenu?.Invoke();
        }
    }
}