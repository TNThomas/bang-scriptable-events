using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.Events;

namespace Bang.Events
{
    public class EventListenerBehaviour : MonoBehaviour, IEventListener
    {
        [Tooltip("Event to register with.")]
        [SerializeField]
        private ScriptableEvent _event;
        public ScriptableEvent Event
        {
            get => _event;
            set => _event = value;
        }

        [Tooltip("Unity Event to perform when the Scriptable Event is raised.")]
        [SerializeField]
        private UnityEvent response;
        public UnityEvent Response => response;

        private void OnEnable()
        {
            response ??= new UnityEvent();
            _event?.RegisterListener(this);
        }

        private void OnDisable()
        { _event?.UnregisterListener(this); }

        public void OnEventRaised()
        { response?.Invoke(); }
    }
}
