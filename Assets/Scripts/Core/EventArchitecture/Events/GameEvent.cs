using System.Collections.Generic;
using System.Linq;
using TripleTriad.Core.EventArchitecture.Listeners;
using UnityEngine;

namespace TripleTriad.Core.EventArchitecture.Events
{
    public abstract class GameEvent<T> : GameEvent 
    {
        [SerializeField] protected T _debugValue = default(T);
        private readonly List<IGameEventListener<T>> _eventListeners = new();

        public bool HasListeners()
        {
            return _eventListeners.Any();
        } 
        
        public override void RaiseTestEvent()
        {
            RaiseEvent(_debugValue);
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
           if(!_eventListeners.Contains(listener))
               _eventListeners.Add(listener);
        }
        
        public void UnregisterListener(IGameEventListener<T> listener)
        {
           if(_eventListeners.Contains(listener))
               _eventListeners.Remove(listener);
        }

        public virtual void RaiseEvent(T value)
        {
            if (!HasListeners())
                Debug.Log($"Event dispatched {GetType().Name} value: {value.ToString()}");
                
            for (int i = _eventListeners.Count - 1; i>= 0; i--)
            {
                _eventListeners[i].OnEventRaise(value);
            }
        }
    }

    public abstract class GameEvent : ScriptableObject 
    {
        public abstract void RaiseTestEvent();
    }
    
}