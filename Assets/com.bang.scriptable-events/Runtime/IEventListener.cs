using UnityEngine.Events;

namespace Bang.Events
{
    public interface IEventListener
    {
        ScriptableEvent Event { get; set; }
        UnityEvent Response { get; }

        void OnEventRaised();
    }
}
