using UnityEngine;
using UnityEngine.Events;

namespace TripleTriad.Assets.Scripts.SOs
{
    [CreateAssetMenu(menuName = "TripleTriad/Core/OnLevelFinishedEventChannel", fileName = "OnLevelFinished Event Channel")]
    public class OnLevelFinishedEventChannel : ScriptableObject
    {
        public UnityEvent<MapLevelData> OnEventRaise;

        public void RaiseEvent(MapLevelData level)
        {
            OnEventRaise?.Invoke(level);
        }
    }
}
