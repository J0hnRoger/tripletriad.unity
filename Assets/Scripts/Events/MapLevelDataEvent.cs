using TripleTriad.Core.EventArchitecture.Events;
using UnityEngine;

namespace TripleTriad.Events
{
    [CreateAssetMenu(menuName = "Game Events/MapLevelEvent", fileName = "MapLevelEvent")]
    public class MapLevelDataEvent : GameEvent<MapLevelData> { }
}