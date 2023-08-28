using System.Collections.Generic;
using Core.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Guesses.View
{
    public class GuessesView : MonoBehaviour
    {
        [SerializeField] private GuessMark _mark;
        [SerializeField] private Transform _markPlaceHolder;

        private List<GuessMark> _marks = new();

        public void Draw(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GuessMark mark = Instantiate(_mark, _markPlaceHolder);
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
            _marks[index].SetMarked();
        }
    }
}