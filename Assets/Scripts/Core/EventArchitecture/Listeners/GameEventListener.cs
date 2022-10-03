using TripleTriad.Core.EventArchitecture.Events;
using UnityEngine;
using UnityEngine.Events;

namespace TripleTriad.Core.EventArchitecture.Listeners
{
    /// <summary>
    /// Listen
    /// </summary>
    /// <typeparam name="T">the type </typeparam>
    /// <typeparam name="E">the Event </typeparam>
    /// <typeparam name="UER">the Unity Event Response</typeparam>
    public class GameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T>
        where E : GameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E _gameEvent;
        [SerializeField] private UER _unityEventResponse;

        private void OnEnable()
        {
            if (_gameEvent == null) return;
            _gameEvent.RegisterListener(this);
        }
      
        private void OnDisable()
        {
            if (_gameEvent == null) return;
            _gameEvent.UnregisterListener(this);
        }
    
        public void OnEventRaise(T value)
        {
            if (_unityEventResponse != null)
                _unityEventResponse.Invoke(value);
        }
    }
}