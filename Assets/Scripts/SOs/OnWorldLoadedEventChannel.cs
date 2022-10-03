using UnityEngine;
using UnityEngine.Events;

namespace TripleTriad.Assets.Scripts.SOs
{
    [CreateAssetMenu(menuName = "TripleTriad/Core/OnWorldLoadedEventChannel", fileName = "OnWorldLoaded Event Channel")]
    public class OnWorldLoadedLoadedEventChannel : ScriptableObject
    {
        public UnityEvent OnEventRaise;

        public void RaiseEvent(PlayerData player, PlayerData opponent)
        {
            OnEventRaise?.Invoke();
        }
    }
}
