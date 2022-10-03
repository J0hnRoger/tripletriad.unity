using UnityEngine;

namespace TripleTriad.Core.EventArchitecture.Events
{
    [CreateAssetMenu(fileName = "Bool Event", menuName = "Game Events/Bool Event")]
    public class BoolEvent : GameEvent<bool>
    { }
}