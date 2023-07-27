using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Differs.Components
{
    public class Differ : MonoBehaviour
    {
        [SerializeField] private Image _correctMarker;
        [SerializeField] private Button _button;

        public Action<Differ> OnClicked;

        private bool _isMarked;

        public bool IsMarked => _isMarked;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
            _correctMarker.gameObject.SetActive(false);
        }

        public void Show()
        {
            _isMarked = true;
            _correctMarker.gameObject.SetActive(true);
        }

        public void OnClick()
        {
            if (_isMarked) return;
            
            OnClicked?.Invoke(this);
        }
    }
}