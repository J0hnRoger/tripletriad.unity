using UnityEngine;
using UnityEngine.Events;

namespace TripleTriad.Assets.Scripts.Core.SOs
{

    [CreateAssetMenu(menuName = "Core/EventChannel", fileName = "Event Variable")]
    public class EventChannel : ScriptableObject
    {
        public UnityEvent OnEventRaise;

        public void RaiseEvent()
        {
            OnEventRaise?.Invoke();
        }
    }
}
