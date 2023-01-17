using UnityEditor;
using UnityEngine;

namespace TWCore.Events
{
    [CustomEditor(typeof(IntEvent), editorForChildClasses: true)]
    public class IntEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameObjectEvent e = target as GameObjectEvent;
            // TODO:
            //if (GUILayout.Button("Raise"))
            //    e.Raise();
        }
    }
}
