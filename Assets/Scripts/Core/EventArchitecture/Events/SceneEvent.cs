using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TripleTriad.Core.EventArchitecture.Events
{
    [Serializable]
    public class SceneInstance
    {
        public SceneName SceneName;  
        public bool IsAdditive;
        public Action OnLoaded;
        public override string ToString()
        {
            return SceneName.ToString();
        }
    } 
    
    [CreateAssetMenu(fileName = "Scene Event", menuName = "Game Events/Scene Event")]
    public class SceneEvent : GameEvent<SceneInstance> { }
}