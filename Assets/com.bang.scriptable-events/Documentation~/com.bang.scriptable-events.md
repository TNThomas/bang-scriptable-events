# Bang! Scriptable Events
Decouple events from their listeners with this ScriptableObject-based microframework! Attach an EventListenerBehaviour to a GameObject and set up all your callbacks right from the inspector. In Play mode, you can view an event's registered listeners and fire the event at the push of a button.

## Installation
Scriptable Events is used through [Unity's Package Manager](https://docs.unity3d.com/Manual/CustomPackages.html). In order to use it you'll need to add the following lines to your `Packages/manifest.json` file. After that you'll be able to visually control what specific version of Scriptable Events you're using from the package manager window in Unity. This has to be done so your Unity editor can connect to NPM's package registry.

```json
{
  "scopedRegistries": [
    {
      "name": "NPM",
      "url": "https://registry.npmjs.org",
      "scopes": [
        "com.bang"
      ]
    }
  ],
  "dependencies": {
    "com.bang.scriptable-events": "1.0.0"
  }
}
```

### Releases
Archives of specific versions and release notes are available on the [releases page](https://github.com/TNThomas/bang-scriptable-events/releases).

## Requirements
Built with Unity 2021 LTS