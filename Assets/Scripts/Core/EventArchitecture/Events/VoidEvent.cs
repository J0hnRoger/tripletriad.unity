using UnityEngine;

namespace TripleTriad.Core.EventArchitecture.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void Event")]
    public class VoidEvent : GameEvent<Void>
    {
        public void RaiseEvent() => RaiseEvent(new Void()); 
    }
}