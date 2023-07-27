using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Differs.Components
{
    public class DiffersFrame : MonoBehaviour
    {
        private List<Differ> _correctPanels;

        public Action<int> OnPanelClicked;

        public int CorrectPanelsCount => _correctPanels.Count;

        private void Awake()
        {
            _correctPanels = new(GetComponentsInChildren<Differ>());
            
            _correctPanels.ForEach(panel => panel.OnClicked += OnPanelClick);
        }

        private void OnDisable()
        {
            _correctPanels.ForEach(panel => panel.OnClicked -= OnPanelClick);
        }

        private void OnPanelClick(Differ panel)
        {
            OnPanelClicked?.Invoke(_correctPanels.IndexOf(panel));
            ShowPanel(panel);
        }

        public int DisplayRandomPanelReturnIndex()
        {
            int randomIndex = Random.Range(0, _correctPanels.Count);
            Differ differ = _correctPanels[randomIndex];

            if (differ.IsMarked)
            {
                DisplayRandomPanelReturnIndex();
            }

            return randomIndex;
        }

        public void DisplayPanelByIndex(int index)
        {
            ShowPanel(_correctPanels[index]);
        }

        private void ShowPanel(Differ panel)
        {
            panel.Show();
        }
    }
}