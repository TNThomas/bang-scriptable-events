using System.Collections.Generic;
using UnityEngine;

namespace Bang.Events
{
    [CreateAssetMenu(fileName = "NewScriptEvent", menuName = "Events/Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        [HideInInspector]
        [SerializeReference]
        private List<IEventListener> listeners = new List<IEventListener>();

        public bool HasListener(IEventListener listener)
        {
            return listeners.Contains(listener);
        }

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(IEventListener listener)
        {
            if ((listener != null)
                && (!listeners.Contains(listener)))
            {
                listeners.Add(listener);
            }
        }

        public bool UnregisterListener(IEventListener listener)
        {
            return listeners.Remove(listener);
        }
    }
}
