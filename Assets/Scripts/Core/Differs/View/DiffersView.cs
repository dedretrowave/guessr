using System;
using System.Collections.Generic;
using Core.Differs.Components;
using UnityEngine;

namespace Core.Differs.View
{
    public class DiffersView : MonoBehaviour
    {
        [SerializeField] private List<DiffersFrame> _frames;
        [SerializeField] private List<WrongPanel> _wrong;

        public Action<int> OnCorrectDifferClicked;
        public Action OnWrongPanelClicked;

        private void Awake()
        {
            _frames.ForEach(frame =>
            {
                frame.OnPanelClicked += OnClick;
            });

            _wrong.ForEach(panel => panel.OnClicked += OnClicked);
        }

        private void OnDestroy()
        {
            _frames.ForEach(frame =>
            {
                frame.OnPanelClicked -= OnClick;
            });
            
            _wrong.ForEach(panel => panel.OnClicked -= OnClicked);
        }

        private void OnClicked()
        {
            OnWrongPanelClicked?.Invoke();
        }

        public int OnRandomGuessMarkAndReturn()
        {
            return _frames[0].DisplayRandomPanelReturnIndex();
        }

        public void SyncCorrectPanels(int index)
        {
            _frames.ForEach(frame => frame.DisplayPanelByIndex(index));
        }

        private void OnClick(int correctPanelIndex)
        {
            OnCorrectDifferClicked?.Invoke(correctPanelIndex);
        }
    }
}