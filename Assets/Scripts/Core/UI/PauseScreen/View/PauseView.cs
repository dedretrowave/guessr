using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.PauseScreen.View
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Button _open;
        [SerializeField] private Button _close;
        [SerializeField] private Button _backToMenu;

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
            _body.gameObject.SetActive(true);
            OnOpen?.Invoke();
        }

        private void Hide()
        {
            _body.gameObject.SetActive(false);
            OnClose?.Invoke();
        }

        private void BackToMenu()
        {
            OnBackToMenu?.Invoke();
        }
    }
}