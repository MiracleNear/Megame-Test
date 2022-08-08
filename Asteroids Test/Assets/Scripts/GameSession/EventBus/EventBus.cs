using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace GameSession
{
    public static class EventBus
    {
        private static Dictionary<Type, List<Object>> _listeners = new Dictionary<Type, List<Object>>();

        public static void Subscribe<TEvent>(Object listener) where TEvent : IGlobalEvent
        {
            Type key = typeof(TEvent);

            if (!_listeners.ContainsKey(key))
            {
                _listeners.Add(key, new List<Object>());
            }

            if (!_listeners[key].Contains(listener))
            {
                _listeners[key].Add(listener);
            }
        }

        public static void UnSubscribe<TEvent>(Object listener) where TEvent : IGlobalEvent
        {
            Type key = typeof(TEvent);

            if (_listeners[key].Contains(listener))
            {
                _listeners[key].Remove(listener);
            }
        }

        public static void Send<TEvent>(Action<TEvent> action) where  TEvent : class, IGlobalEvent
        {
            foreach (Object listener in _listeners[typeof(TEvent)])
            {
                try
                {
                    action.Invoke(listener as TEvent);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
    }
}