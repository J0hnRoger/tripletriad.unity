using UnityEngine;
using UnityEngine.Events;

namespace TripleTriad.Assets.Scripts.SOs
{
    [CreateAssetMenu(menuName = "TripleTriad/Core/OnLevelVisitedEventChannel", fileName = "OnLevelVisited Event Channel")]
    public class OnLevelVisitedEventChannel : ScriptableObject
    {
        public UnityEvent<MapLevelData> OnEventRaise;

        public void RaiseEvent(MapLevelData level)
        {
            OnEventRaise?.Invoke(level);
        }
    }
}
