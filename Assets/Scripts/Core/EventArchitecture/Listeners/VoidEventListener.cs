using TripleTriad.Core.EventArchitecture.Events;
using TripleTriad.Core.EventArchitecture.UnityEvents;

namespace TripleTriad.Core.EventArchitecture.Listeners
{
    public class VoidEventListener : GameEventListener<Void, VoidEvent, UnityVoidEvent>
    {
    }
}