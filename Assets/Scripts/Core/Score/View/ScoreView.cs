using TMPro;
using UnityEngine;

namespace Core.Score.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreLabel;

        public void SetScore(int score)
        {
            _scoreLabel.text = score.ToString();
        }
    }
}