using System.Collections.Generic;
using UnityEngine;

namespace Bang.Events
{
    public class EventListenerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<UnityEventCallback> listeners;

        private void OnEnable()
        {
            Debug.Log("Enabling listeners");
            foreach (EventListener listener in listeners)
            {
                Debug.Log(listener.Event);
                listener.Enabled = (listener.Event != null);
                Debug.Log(listener.Enabled);
            }
        }

        private void OnDisable()
        {
            foreach (EventListener listener in listeners)
            {
                listener.Enabled = false;
            }
        }
    }
}
