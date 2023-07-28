using System.Collections.Generic;
using EventBus;
using UnityEngine;

namespace Core.UI.WinScreen
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private WinPopup _popup;
        [SerializeField] private List<Transform> _spawnPoints;

        private EventBus.EventBus _eventBus;

        private void Awake()
        {
            _eventBus = EventBus.EventBus.Instance;
            
            _eventBus.AddListener(EventName.ON_ALL_DIFFERS_FOUND, SpawnPopup);
        }

        private void OnDisable()
        {
            _eventBus.RemoveListener(EventName.ON_ALL_DIFFERS_FOUND, SpawnPopup);
        }

        private void SpawnPopup()
        {
            WinPopup popup = Instantiate(
                _popup,
                _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
        }
    }
}