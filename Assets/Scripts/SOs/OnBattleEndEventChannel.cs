using UnityEngine;
using UnityEngine.Events;

namespace TripleTriad.Assets.Scripts.SOs
{
    [CreateAssetMenu(menuName = "TripleTriad/Core/OnBattleEndEventChannel", fileName = "OnBattleEnd Event Channel")]
    public class OnBattleEndEventChannel : ScriptableObject
    {
        public UnityEvent<bool> OnEventRaise;

        public void RaiseEvent(bool hasWin)
        {
            OnEventRaise?.Invoke(hasWin);
        }
    }
}
