using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TripleTriad.Assets.Scripts.SOs
{
    [CreateAssetMenu(menuName = "TripleTriad/Core/OnWorldStartedEventChannel", fileName = "OnWorldStarted Event Channel")]
    public class OnWorldStartedEventChannel : ScriptableObject
    {
        public UnityEvent<List<MapLevelData>> OnEventRaise;

        public void RaiseEvent(List<MapLevelData> levels)
        {
            OnEventRaise?.Invoke(levels);
        }
    }
}
