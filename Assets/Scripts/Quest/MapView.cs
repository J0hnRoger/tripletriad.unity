using System;
using System.Collections.Generic;
using TMPro;
using TripleTriad.Core.EventArchitecture;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private SceneEventDispatcher _sceneDispatcher; 
    [SerializeField] private MapNodeData _runtimeCurrentBattle;
    
    [SerializeField] private MapLevelData _mapLevelData;
    [SerializeField] private GameObject _nodeTemplate;
    [SerializeField] private Transform _startingPoint;

    public Action<MapLevelData> OnLevelFinished;
    public Action<PlayerData> OnVisitNode;

    private TMP_Text _mapTitle;
    [SerializeField] private LineRenderer _activeLineRenderer;
    [SerializeField] private LineRenderer _availableLineRenderer;

    private List<MapNodeView> _allNodes = new List<MapNodeView>();
    [HideInInspector] public bool IsLastLevel { get; private set; } 

    private void Awake()
    {
        _mapTitle = GetComponentInChildren<TMP_Text>();
        InitMap(_mapLevelData);
    }

    public void InitMap(MapLevelData mapLevel)
    {
        _mapLevelData = mapLevel;
        CreateNodes(_mapLevelData.RuntimeNodes);
        _mapTitle.SetText(_mapLevelData.LevelName);
    }

    private void CreateNodes(List<MapNodeData> nodes)
    {
        // On genrre un node � chaque ligne, en fonction du nombre d'�tage pr�vu et de la taille total
        float distance = 1.5f;
        float nextY = _startingPoint.position.y;
        // generate line behind the nodes

        Vector3 activeLinesStartingPoint = new Vector3(_startingPoint.position.x,
            _startingPoint.position.y, _startingPoint.position.z + 0.3f);
        _activeLineRenderer.positionCount = 1;
        _activeLineRenderer.SetPosition(0, activeLinesStartingPoint);

        Vector3 availableLinesStartingPoint = new Vector3(_startingPoint.position.x,
            _startingPoint.position.y, _startingPoint.position.z + 0.5f);
        _availableLineRenderer.positionCount = nodes.Count; 

        int lineIndex = 0;
        // _availableLineRenderer.SetPosition(lineIndex, availableLinesStartingPoint);
        foreach (MapNodeData node in nodes)
        {
             float nextX = lineIndex == 0 ? _startingPoint.position.x 
                 : lineIndex % 2 == 0 ?
                     _startingPoint.position.x + 1 
                     : _startingPoint.position.x - 1;

             if (lineIndex == nodes.Count) // last 
                nextX = _startingPoint.position.x; 
            //float nextX = _startingPoint.position.x;
            var newNode = Instantiate(_nodeTemplate,
                new Vector3(nextX, nextY, _startingPoint.position.z),
                Quaternion.identity, gameObject.transform);

            var nodeView = newNode.GetComponent<MapNodeView>();
            nodeView.SetNodeData(node);
            
            Vector3 linePosition = new Vector3(nextX,
                newNode.transform.position.y, newNode.transform.position.z + 0.5f);

            _availableLineRenderer.SetPosition(lineIndex, linePosition);
            _allNodes.Add(nodeView);
            nodeView.OnVisitedNode += VisitNodeIfPossible;

            lineIndex++;
            nextY += distance;
        }
    }

    private void VisitNodeIfPossible(MapNodeView targetNode, PlayerData opponent)
    {
        if (_mapLevelData.CanVisit(targetNode.Node))
        {
            DrawPathToNode(targetNode, opponent);
            targetNode.VisitNode();
            _runtimeCurrentBattle.Set(targetNode.Node); 
            _sceneDispatcher.LoadScene();
        }
    }

    private void DrawPathToNode(MapNodeView targetNode, PlayerData data)
    {
        int index = _allNodes.IndexOf(targetNode);
        _activeLineRenderer.positionCount += 1;

        Vector3 linePosition = new Vector3(targetNode.transform.position.x,
            targetNode.transform.position.y, targetNode.transform.position.z + 0.3f);
        _activeLineRenderer.SetPosition(index + 1, linePosition);

        _mapLevelData.VisitNode(targetNode.Node);
        IsLastLevel = index == _allNodes.Count - 1;
    }
}

