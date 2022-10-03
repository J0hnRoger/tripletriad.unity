using UnityEngine;
using UnityEngine.Events;

namespace TripleTriad.Assets.Scripts.SOs
{
    [CreateAssetMenu(menuName = "TripleTriad/Core/OnNodeVisitedEventChannel", fileName = "OnNodeVisited Event Channel")]
    public class OnNodeVisitedEventChannel : ScriptableObject
    {
        public UnityEvent<PlayerData> OnEventRaise;

        public void RaiseEvent(PlayerData opponent)
        {
            OnEventRaise?.Invoke(opponent);
        }
    }
}
