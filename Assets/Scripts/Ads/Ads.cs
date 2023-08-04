using System;
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

        private void Awake()
        {
            _eventBus = EventBus.EventBus.Instance;
        }

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

        public void InvokeAdWatched()
        {
            _eventBus.TriggerEvent(EventName.ON_AD_WATCHED);
        }

        public void InvokeRewardedWatched()
        {
            _eventBus.TriggerEvent(EventName.ON_REWARDED_WATCHED);
        }

        public void InvokeRewardedSkipped()
        {
            _eventBus.TriggerEvent(EventName.ON_REWARDED_SKIPPED);
        }
    }
}