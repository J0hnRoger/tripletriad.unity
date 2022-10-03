using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "MapNode", menuName = "TripleTriad/MapNode")]
public class MapNodeData : ScriptableObject
{
    public Sprite ActiveNodeImage;
    public Sprite InactiveNodeImage;
    public PlayerData Opponent;
    public bool IsVisited;

    public void Set(MapNodeData other)
    {
        ActiveNodeImage = other.ActiveNodeImage;
        InactiveNodeImage = other.InactiveNodeImage;
        Opponent = other.Opponent;
        IsVisited = other.IsVisited;
    }
}
