using System.Runtime.InteropServices;
using EventBus;
using UnityEngine;

namespace Ads
{
    public class Ads : MonoBehaviour
    {
        private EventBus.EventBus _eventBus;
        
        [DllImport("__Internal")]
        private static extern void ShowAdExternal();

        [DllImport("__Internal")]
        private static extern void ShowRewardedExternal();

        public void ShowRewarded()
        {
#if  !UNITY_EDITOR
            ShowRewardedExternal();      
#endif
        }

        public void ShowAd()
        {
#if  !UNITY_EDITOR
        ShowAdExternal();
#else
            Debug.Log("Very interesting Ad!");
#endif
        }

        private void InvokeAdWatched()
        {
            _eventBus.TriggerEvent(EventName.ON_AD_WATCHED);
        }

        private void InvokeRewardedWatched()
        {
            _eventBus.TriggerEvent(EventName.ON_REWARDED_WATCHED);
        }

        private void InvokeRewardedSkipped()
        {
            _eventBus.TriggerEvent(EventName.ON_REWARDED_SKIPPED);
        }
    }
}