using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNodeView : MonoBehaviour, IPointerDownHandler
{
    public Action<MapNodeView, PlayerData> OnVisitedNode;
    [SerializeField] private SpriteRenderer _iconSpriteRenderer;
    [SerializeField] private GameObject _visitedIconGO;
    [SerializeField] private GameObject _availableIconGO;

    public MapNodeData Node;
  
    public void VisitNode()
    {
        _iconSpriteRenderer.sprite = Node.ActiveNodeImage;
    }

    public void SetNodeData(MapNodeData node)
    {
        Node = node;
        _visitedIconGO.SetActive(node.IsVisited);
        _iconSpriteRenderer.sprite = (node.IsVisited) ? node.ActiveNodeImage : node.InactiveNodeImage;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnVisitedNode?.Invoke(this, Node.Opponent);
    }
}
