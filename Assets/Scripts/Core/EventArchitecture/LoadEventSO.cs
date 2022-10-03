using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.Events;

namespace TripleTriad.Core.EventArchitecture
{
    [CreateAssetMenu(fileName = "LoadEvent", menuName = "Game Events/LoadEvent")]
    public class LoadEventSO : ScriptableObject
    {
        public UnityAction<GameSceneSO[], bool> OnLoadingRequest;
        
        public void RaiseEvent(GameSceneSO[] scenesToLoad, bool showLoadingScreen)
        {
            if (OnLoadingRequest == null)
            {
                Debug.LogWarning("Scene loading request - but no listeners");
                return;
            } 
            
            OnLoadingRequest.Invoke(scenesToLoad, showLoadingScreen);
        } 
    }
}