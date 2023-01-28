using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Bang.Events.Tests
{
    public class ScriptableEventTest
    {
        private GameObject gameObject;
        EventListenerBehaviour listener;
        EventListenerBehaviour listener2;
        ScriptableEvent testEvent;

        [SetUp]
        public void BeforeEach()
        {
            gameObject = new GameObject("eventHost");
            listener = gameObject.AddComponent<EventListenerBehaviour>();
            listener2 = gameObject.AddComponent<EventListenerBehaviour>();
            testEvent = ScriptableObject.CreateInstance<ScriptableEvent>();
        }
        [TearDown]
        public void AfterEach()
        {
            Object.DestroyImmediate(gameObject);
        }

        public class RegisterListener : ScriptableEventTest
        {
            [Test]
            public void DoNotRegisterNullListener()
            {
                testEvent.RegisterListener(null);
                Assert.IsFalse(testEvent.HasListener(null));
            }
            [Test]
            public void RegisterValidListener()
            {
                testEvent.RegisterListener(listener);
                Assert.IsTrue(testEvent.HasListener(listener));
            }
            [Test]
            public void RegisterMultipleListeners()
            {
                testEvent.RegisterListener(listener);
                testEvent.RegisterListener(listener2);
                Assert.IsTrue(testEvent.HasListener(listener));
                Assert.IsTrue(testEvent.HasListener(listener2));
            }
        }

        public class UnregisterListener : ScriptableEventTest
        {
            [SetUp]
            public void RegisterTestListener()
            {
                testEvent.RegisterListener(listener);
                testEvent.RegisterListener(listener2);
            }
            [TearDown]
            public void UnregisterTestListener()
            {
                testEvent.UnregisterListener(listener);
                testEvent.UnregisterListener(listener2);
            }
            [Test]
            public void RemovePresentListener()
            {
                testEvent.UnregisterListener(listener);
                Assert.IsFalse(testEvent.HasListener(listener));
                Assert.IsTrue(testEvent.HasListener(listener2));
            }
        }

        public class Raise : ScriptableEventTest
        {
            bool listenerInvoked = false;
            bool listener2Invoked = false;

            [SetUp]
            public void ConfigureTestListener()
            {
                listenerInvoked = false;
                listener2Invoked = false;
                UnityAction action = new UnityAction(() => listenerInvoked = true);
                UnityAction action2 = new UnityAction(() => listener2Invoked = true);
                listener.Response.AddListener(action);
                listener2.Response.AddListener(action2);
                testEvent.RegisterListener(listener);
                testEvent.RegisterListener(listener2);
            }
            [Test]
            public void InvokeListener()
            {
                testEvent.Raise();
                Assert.IsTrue(listenerInvoked);
            }
            [Test]
            public void InvokeMultipleListeners()
            {
                testEvent.Raise();
                Assert.IsTrue(listenerInvoked);
                Assert.IsTrue(listener2Invoked);
            }
        }
    }
}
