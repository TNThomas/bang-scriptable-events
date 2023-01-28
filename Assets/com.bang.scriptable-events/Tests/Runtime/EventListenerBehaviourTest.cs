using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Bang.Events.Tests
{
    public class EventListenerBehaviourTest
    {
        private GameObject gameObject;
        private ScriptableEvent scriptEvent;
        private EventListenerBehaviour testEventListener;

        [SetUp]
        public void BeforeEach()
        {
            gameObject = new GameObject("eventHost");
            scriptEvent = ScriptableObject.CreateInstance<ScriptableEvent>();
            testEventListener = gameObject.AddComponent<EventListenerBehaviour>();
        }
        [TearDown]
        public void AfterEach()
        {
            Object.DestroyImmediate(gameObject);
        }

        public class OnEnable : EventListenerBehaviourTest
        {
            [Test]
            public void RegisterOnEnable()
            {
                testEventListener.enabled = false;
                testEventListener.Event = scriptEvent;
                testEventListener.enabled = true;
                Assert.IsTrue(testEventListener.Event.HasListener(testEventListener));
            }
        }
        public class OnDisable : EventListenerBehaviourTest {
            [SetUp]
            public void Register()
            {
                testEventListener.Event = scriptEvent;
                scriptEvent.RegisterListener(testEventListener);
            }
            [TearDown]
            public void Unregister()
            {
                testEventListener.Event = null;
                scriptEvent.UnregisterListener(testEventListener);
            }
            [Test]
            public void UnregisterOnDisable()
            {
                testEventListener.Event = scriptEvent;
                testEventListener.enabled = false;
                Assert.IsFalse(testEventListener.Event.HasListener(testEventListener));
            }
        }
        public class OnEventRaised : EventListenerBehaviourTest
        {
            bool responseInvoked = false;
            [SetUp]
            public void ConfigureResponse()
            {
                responseInvoked = false;
                UnityAction action = new UnityAction(() => responseInvoked = true);
                testEventListener.Response.AddListener(action);
            }
            [Test]
            public void RaiseInvokesResponse()
            {
                testEventListener.OnEventRaised();
                Assert.IsTrue(responseInvoked);
            }
        }
    }
}
