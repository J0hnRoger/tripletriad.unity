using System;
using TripleTriad.Core.EventArchitecture.Events;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TripleTriad.Core.EventArchitecture.UnityEvents
{
    [Serializable]
    public class UnitySceneEvent : UnityEvent<SceneInstance>
    {
        
    }
}