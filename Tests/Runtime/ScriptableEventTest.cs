using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Bang.Events.Tests
{
    public class ScriptableEventTest
    {
        private GameObject gameObject;
        EventListener<InvokableUnityEvent> listener;
        EventListener<InvokableUnityEvent> listener2;
        ScriptableEvent testEvent;

        [SetUp]
        public void BeforeEach()
        {
            gameObject = new GameObject("eventHost");
            listener = new EventListener<InvokableUnityEvent>();
            listener2 = new EventListener<InvokableUnityEvent>();
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
                listener.Event = testEvent;
                listener2.Event = testEvent;
                listener.Enabled = true;
                listener2.Enabled = true;
                listener.Response.AddListener(new UnityAction(() => listenerInvoked = true));
                listener2.Response.AddListener(new UnityAction(() => listener2Invoked = true));
            }
            [Test]
            public void InvokeListeners()
            {
                testEvent.Raise();
                Assert.IsTrue(listenerInvoked);
                Assert.IsTrue(listener2Invoked);
            }
        }
    }
}
