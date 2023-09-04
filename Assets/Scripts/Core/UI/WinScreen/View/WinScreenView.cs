using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.WinScreen.View
{
    public class WinScreenView : MonoBehaviour
    {
        [SerializeField] private WinLabel _label;
        [SerializeField] private Animator _animator;

        [SerializeField] private Button _back;
        [SerializeField] private Button _next;
        [SerializeField] private TextMeshProUGUI _allLevelsPassedText;
        [SerializeField] private List<GameObject> _allLevelsPassedDisables;

        private readonly int _isShown = Animator.StringToHash("IsShown");

        public Action OnBack;
        public Action OnNext;

        private void Awake()
        {
            _back.onClick.AddListener(OnBackPressed);
            _next.onClick.AddListener(OnNextPressed);
        }

        public void ShowAllLevelsPassed()
        {
            Show();
            
            _allLevelsPassedDisables.ForEach(item => item.SetActive(false));
            
            _allLevelsPassedText.gameObject.SetActive(true);
        }

        public void Show()
        {
            _animator.SetBool(_isShown, true);
            _label.Show();
        }

        public void Hide()
        {
            _animator.SetBool(_isShown, false);
        }

        public void OnBackPressed()
        {
            OnBack?.Invoke();
        }

        public void OnNextPressed()
        {
            OnNext?.Invoke();
        }
    }
}