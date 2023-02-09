using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bang.Events.Tests
{
    public class EventListenerTest
    {
        private ScriptableEvent scriptEvent;
        private EventListener<InvokableUnityEvent> testListener;

        [SetUp]
        public void BeforeEach()
        {
            scriptEvent = ScriptableObject.CreateInstance<ScriptableEvent>();
            testListener = new EventListener<InvokableUnityEvent>();
        }

        public class Disable : EventListenerTest
        {
            [SetUp]
            public void EnableListener()
            {
                testListener.Event = scriptEvent;
                testListener.Enabled = true;
            }

            [Test]
            public void SetDisabled()
            {
                testListener.Enabled = false;
                Assert.IsFalse(testListener.Enabled);
            }
            [Test]
            public void UnregisterEvent()
            {
                testListener.Enabled = false;
                Assert.IsFalse(scriptEvent.HasListener(testListener));
            }
        }

        public class Enable : EventListenerTest
        {
            [SetUp]
            public void AddDisabledEvent()
            {
                testListener.Event = scriptEvent;
            }

            [Test]
            public void SetEnabled() {
                testListener.Enabled = true;
                Assert.IsTrue(testListener.Enabled);
            }
            [Test]
            public void RegisterListener()
            {
                testListener.Enabled = true;
                Assert.IsTrue(scriptEvent.HasListener(testListener));
            }
            [Test]
            public void NoEnablingNullEvents()
            {
                testListener.Event = null;
                Assert.Throws<InvalidOperationException>(() => testListener.Enabled = true);
            }
        }
        public class OnEventRaised : EventListenerTest
        {
            bool responseInvoked;

            [SetUp]
            public void ConfigureEvent()
            {
                responseInvoked = false;
                testListener.Response.AddListener(new(() => responseInvoked = true));
                testListener.Event = scriptEvent;
            }

            [Test]
            public void InvokeOnEnabled()
            {
                testListener.Enabled = true;
                testListener.OnEventRaised();
                Assert.IsTrue(responseInvoked);
            }
            [Test]
            public void NoInvokeIfDisabled()
            {
                testListener.Enabled = false;
                testListener.OnEventRaised();
                Assert.IsFalse(responseInvoked);
            }
        }

        public class SetEvent : EventListenerTest
        {
            ScriptableEvent scriptEvent2;

            [SetUp]
            public void ConfigureEvent()
            {
                scriptEvent2 = ScriptableObject.CreateInstance<ScriptableEvent>();
            }

            [Test]
            public void UpdateValue()
            {
                testListener.Event = scriptEvent;
                Assert.IsTrue(testListener.Event == scriptEvent);
                testListener.Event = null;
                Assert.IsNull(testListener.Event);
            }
            [Test]
            public void RegisterIfEnabled()
            {
                testListener.Event = scriptEvent2;
                testListener.Enabled = true;
                testListener.Event = scriptEvent;
                Assert.IsTrue(scriptEvent.HasListener(testListener));
            }
            [Test]
            public void NoRegisterIfDisabled()
            {
                testListener.Enabled = false;
                testListener.Event = scriptEvent;
                Assert.IsFalse(scriptEvent.HasListener(testListener));
            }
            [Test]
            public void UnregisterOldEvent()
            {
                testListener.Event = scriptEvent;
                testListener.Enabled = true;
                testListener.Event = scriptEvent2;
                Assert.IsFalse(scriptEvent.HasListener(testListener));
                Assert.IsTrue(scriptEvent2.HasListener(testListener));
            }
            [Test]
            public void PersistEnabled()
            {
                testListener.Event = scriptEvent;
                testListener.Enabled = true;
                testListener.Event = scriptEvent2;
                Assert.IsTrue(testListener.Enabled);
            }
            [Test]
            public void DisableIfNullEvent()
            {
                testListener.Event = scriptEvent;
                testListener.Enabled = true;
                testListener.Event = null;
                Assert.IsFalse(testListener.Enabled);
            }
        }
    }
}
