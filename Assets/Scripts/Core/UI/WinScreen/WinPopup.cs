using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Core.UI.WinScreen
{
    public class WinPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private List<string> _textVariants;

        private void Start()
        {
            _text.text = _textVariants[Random.Range(0, _textVariants.Count)];
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}