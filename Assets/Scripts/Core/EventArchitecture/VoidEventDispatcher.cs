using TripleTriad.Core.EventArchitecture.Events;
using UnityEngine;

namespace TripleTriad.Core.EventArchitecture
{
    public class VoidEventDispatcher: MonoBehaviour
    {
        [SerializeField] private VoidEvent _dispatchingEvent;

        public void Dispatch()
        {
            if (_dispatchingEvent == null || !_dispatchingEvent.HasListeners())
                Debug.Log($"Void Event to {_dispatchingEvent.name} dispatched");
            else
                _dispatchingEvent.RaiseEvent();
        }
    }
}