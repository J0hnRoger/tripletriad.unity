using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "MapLevel", menuName = "TripleTriad/MapLevel")]
public class MapLevelData : ScriptableObject
{
    public MapNodeData CurrentNode;

    [SerializeField] private List<MapNodeData> _nodes;
    public string LevelName;

    public bool IsVisited;

    public List<MapNodeData> RuntimeNodes => _nodes;

    public void Set(MapLevelData other)
    {
        IsVisited = other.IsVisited;
        LevelName = other.LevelName;
        _nodes = other.RuntimeNodes;
    }

    public bool CanVisit(MapNodeData visitingNode)
    {
        if (visitingNode.IsVisited)
            return false;

        int index = RuntimeNodes.IndexOf(visitingNode);
        bool canVisit = RuntimeNodes.Take(index)
            .All(n => n.IsVisited);
        return canVisit;
    }

    public void VisitNode(MapNodeData mapNode)
    {
        CurrentNode = mapNode;
    }

    public void CompleteCurrentBattle()
    {
        var currentBattle = _nodes.FirstOrDefault(b => b.IsVisited == false);
        if (currentBattle == null)
            return;
        currentBattle.IsVisited = true;
    }
    
    public bool IsComplete()
    {
        return _nodes.All(b => b.IsVisited);
    }

    public void Reset()
    {
       _nodes.ForEach(n => n.IsVisited = false); 
    }
}
