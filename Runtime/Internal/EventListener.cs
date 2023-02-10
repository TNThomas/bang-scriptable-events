using System;
using UnityEngine;

namespace Bang.Events
{
    [Serializable]
    public abstract class EventListener
    {
        public bool Enabled
        {
            get => @event != null && @event.HasListener(this);
            set
            {
                if (value)
                {
                    if(@event == null)
                    {
                        throw new InvalidOperationException("Cannot enable listening on a null event");
                    }
                    @event.RegisterListener(this);
                }
                else
                {
                    @event?.UnregisterListener(this);
                }
            }
        }

        [SerializeField] private ScriptableEvent @event;
        public ScriptableEvent Event {
            get => @event;
            set
            {
                if (Enabled)
                {
                    @event.UnregisterListener(this);
                    value?.RegisterListener(this);
                }
                @event = value;
            }
        }

        [SerializeField] private IInvokable _response;
        public virtual IInvokable Response {
            get => _response;
        }

        public abstract void OnEventRaised();
    }

    [Serializable]
    public class EventListener<TResponse> : EventListener where TResponse : IInvokable, new()
    {
        [SerializeField] private TResponse _response;
        public EventListener()
        {
            _response = new TResponse();
        }
        public new TResponse Response
        {
            get => _response;
        }

        public override void OnEventRaised()
        {
            if (Enabled)
            {
                Response.Invoke();
            }
        }
    }
}

