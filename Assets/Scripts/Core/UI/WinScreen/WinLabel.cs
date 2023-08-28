using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Core.UI.WinScreen
{
    public class WinLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private List<string> _textVariants;

        public void Show()
        {
            _text.text = _textVariants[Random.Range(0, _textVariants.Count)];
        }
    }
}