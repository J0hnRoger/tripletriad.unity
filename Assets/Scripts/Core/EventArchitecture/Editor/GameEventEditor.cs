using TripleTriad.Assets.Scripts.SOs;
using TripleTriad.Core.EventArchitecture.Events;
using UnityEditor;
using UnityEngine;

namespace TripleTriad.Core.EventArchitecture.Editor
{
    [CustomEditor(typeof(GameEvent), true)]
    public class GameEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GameEvent current = (GameEvent) target;
            if (GUILayout.Button("Raise event"))
            {
               current.RaiseTestEvent();
            }
        }
        
    }
}