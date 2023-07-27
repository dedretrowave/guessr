using EventBus;
using UnityEngine;

namespace Core.Differs.Components
{
    public class Hint : MonoBehaviour
    {
        private EventBus.EventBus _eventBus;

        private void Awake()
        {
            _eventBus = EventBus.EventBus.Instance;
        }

        public void TriggerAd()
        {
            _eventBus.TriggerEvent(EventName.ON_REWARDED_OPEN);
            _eventBus.AddListener(EventName.ON_REWARDED_WATCHED, TriggerHint);
            //TODO: DELETE ON RELEASE
            _eventBus.TriggerEvent(EventName.ON_REWARDED_WATCHED);
        }

        private void TriggerHint()
        {
            _eventBus.RemoveListener(EventName.ON_REWARDED_WATCHED, TriggerHint);
            _eventBus.TriggerEvent(EventName.ON_HINT);
        }
    }
}