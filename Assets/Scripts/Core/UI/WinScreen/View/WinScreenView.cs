using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.WinScreen.View
{
    public class WinScreenView : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private TextMeshProUGUI _totalScoreLabel;
        [SerializeField] private Button _back;

        public Action OnBack;

        private void Awake()
        {
            _back.onClick.AddListener(OnBackPressed);
        }

        private void OnBackPressed()
        {
            OnBack?.Invoke();
        }

        public void DisplayTotalScore(int totalScore)
        {
            _totalScoreLabel.text = totalScore.ToString();
        }

        public void Show()
        {
            _body.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _body.gameObject.SetActive(false);
        }
    }
}