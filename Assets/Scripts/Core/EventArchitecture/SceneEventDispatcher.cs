using TripleTriad.Core.EventArchitecture.Events;
using UnityEngine;

namespace TripleTriad.Core.EventArchitecture
{
    public class SceneEventDispatcher : MonoBehaviour
    {
        [SerializeField] private SceneEvent _onLoadSceneEvent;
        [SerializeField] private SceneName _sceneName;

        public void LoadScene()
        {
            if (_onLoadSceneEvent == null || !_onLoadSceneEvent.HasListeners())
                Debug.Log($"Scene Event to {_sceneName} dispatched");
            else
                _onLoadSceneEvent.RaiseEvent(new SceneInstance()
                {
                   SceneName = _sceneName,
                   IsAdditive = true
                }); 
        }
    }
}