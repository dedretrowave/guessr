using System;
using System.Collections;
using UnityEngine;

namespace EventBus
{
    public class EventBus
    {
        private Hashtable _events = new();

        private static EventBus _instance;

        public static EventBus Instance => _instance ??= new();

        public void AddListener(EventName eventName, Action callback)
        {
            Action currentEvent = null;
            string key = eventName.ToString();

            if (_events.ContainsKey(key))
            {
                currentEvent = (Action)_events[key];
                currentEvent += callback;
                _events[key] = currentEvent;
            }
            else
            {
                currentEvent += callback;
                _events.Add(key, currentEvent);
            }
        }

        public void AddListener<T>(EventName name, Action<T> callback)
        {
            string key = GetKey<T>(name);

            Action<T> currentEvent = null;

            if (_events.ContainsKey(key))
            {
                currentEvent = (Action<T>)_events[key];
                currentEvent += callback;
                _events[key] = currentEvent;
            }
            else
            {
                currentEvent += callback;
                _events.Add(key, currentEvent);
            }
        }
        
        public void RemoveListener<T>(EventName name, Action<T> callback)
        {
            Action<T> currentEvent = null;
            string key = GetKey<T>(name);

            if (_events.ContainsKey(key))
            {
                currentEvent = (Action<T>)_events[key];
                currentEvent -= callback;
                _events[key] = currentEvent;
            }
        }
        
        public void RemoveListener(EventName name, Action callback)
        {
            Action currentEvent = null;
            string key = name.ToString();

            if (_events.ContainsKey(key))
            {
                currentEvent = (Action)_events[key];
                currentEvent -= callback;
                _events[key] = currentEvent;
            }
        }

        public void TriggerEvent<T>(EventName name, T returnedType)
        {
            Action<T> currentEvent;
            string key = GetKey<T>(name);

            if (_events.ContainsKey(key))
            {
                currentEvent = (Action<T>)_events[key];
                currentEvent.Invoke(returnedType);
            }
        }

        public void TriggerEvent(EventName name)
        {
            Action currentEvent;
            string key = name.ToString();

            if (_events.ContainsKey(key))
            {
                try
                {
                    currentEvent = (Action)_events[key];
                    currentEvent.Invoke();
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
        }


        private string GetKey<T>(EventName name)
        {
            Type type = typeof(T);
            string key = type + name.ToString();
            return key;
        }
    }
}