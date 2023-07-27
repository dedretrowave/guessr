using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.LoseScreen.View
{
    public class LoseScreenView : MonoBehaviour
    {
        [SerializeField] private Transform _body;

        [SerializeField] private Button _back;
        [SerializeField] private Button _replay;
        [SerializeField] private Button _moreTime;

        public Action OnBack;
        public Action OnReplay;
        public Action OnMoreTime;

        private void Awake()
        {
            _back.onClick.AddListener(OnBackPressed);
            _replay.onClick.AddListener(OnReplayPressed);
            _replay.onClick.AddListener(OnMoreTimePressed);
        }

        public void Show()
        {
            _body.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _body.gameObject.SetActive(false);
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