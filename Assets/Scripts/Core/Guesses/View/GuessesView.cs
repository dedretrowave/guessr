using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Guesses.View
{
    public class GuessesView : MonoBehaviour
    {
        [SerializeField] private Image _mark;
        [SerializeField] private Transform _markPlaceHolder;
        
        [SerializeField] private Sprite _emptyMark;
        [SerializeField] private Sprite _fullMark;
        
        private List<Image> _marks = new();

        public void Draw(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Image mark = Instantiate(_mark, _markPlaceHolder);
                mark.sprite = _emptyMark;
                _marks.Add(mark);
            }
        }

        public void Clear()
        {
            Image[] marks = _markPlaceHolder.GetComponentsInChildren<Image>();

            if (marks.Length == 0) return;

            foreach (Image image in marks)
            {
                Destroy(image.gameObject);
            }
            
            _marks.Clear();
        }

        public void Mark(int index)
        {
            _marks[index].sprite = _fullMark;
        }
    }
}