using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bang.Events
{
    [CreateAssetMenu(fileName = "NewScriptEvent", menuName = "Events/Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        [HideInInspector]
        [SerializeReference]
        private List<EventListener> listeners = new List<EventListener>();

        public bool HasListener(EventListener listener)
        {
            return listeners.Contains(listener);
        }

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(EventListener listener)
        {
            if ((listener != null)
                && (!listeners.Contains(listener)))
            {
                listeners.Add(listener);
            }
        }

        public bool UnregisterListener(EventListener listener)
        {
            return listeners.Remove(listener);
        }
    }
}
