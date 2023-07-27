using System;
using TMPro;
using UnityEngine;

namespace Core.Timer.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        public void SetTime(int time)
        {
            TimeSpan span = TimeSpan.FromSeconds(time);

            _label.text = span.ToString(@"mm\:ss");
        }
    }
}