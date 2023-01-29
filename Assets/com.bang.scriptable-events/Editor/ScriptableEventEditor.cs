using System;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace Bang.Events.Editors
{
    [CustomEditor(typeof(ScriptableEvent))]
    public class ScriptableEventEditor : Editor
    {
        SerializedProperty listeners;
        private void OnEnable()
        {
            listeners = serializedObject.FindProperty("listeners");
            Debug.Log(listeners?.ToString());
        }
        public override void OnInspectorGUI ()
        {
            DrawDefaultInspector();
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(listeners);
            }
            using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
                if(GUILayout.Button("Fire Event"))
                {
                    (serializedObject.targetObject as ScriptableEvent).Raise();
                }
            }
        }
    }
}
