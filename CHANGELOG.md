# 1.0.0 (2023-02-10)


### Features

* add event and listener classes and tests ([d244c16](https://github.com/TNThomas/bang-scriptable-events/commit/d244c1624f1b74ba8e85b8d429149afd91ad8285))
* enable EventListenerBehaviours to hold an editable list of listeners ([b6a564d](https://github.com/TNThomas/bang-scriptable-events/commit/b6a564dc283b789c6bb002c4fefef8aa024f0f02))
* **scriptableeventeditor.cs:** add custom editor for ScriptableEvent objects ([08a4407](https://github.com/TNThomas/bang-scriptable-events/commit/08a4407e02e234bee4c47900c12a84e827c5324e))


### BREAKING CHANGES

* EventListenerBehaviour no longer implements IEventListener. IEventListener
responses are now of type IInvokable. Class InvokableUnityEvent has been added as a replacement type
for vanilla UnityEvents in this context.
