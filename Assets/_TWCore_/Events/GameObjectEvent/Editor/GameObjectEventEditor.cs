using UnityEditor;
using UnityEngine;

namespace TWCore.Events
{
    [CustomEditor(typeof(GameObjectEvent), editorForChildClasses: true)]
    public class GameObjectEventEditor : Editor
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
