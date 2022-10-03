using System;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldNodeView : MonoBehaviour, IPointerDownHandler
{
    public Action<WorldNodeView> OnVisitedNode;

    [SerializeField] private TMP_Text _levelNameTxt;
    [SerializeField] private Transform _starContainer;
    [SerializeField] private GameObject _starTemplate;
    [SerializeField] private Sprite _starFullIcon;

    public bool IsAvailable { get; set; }

    public MapLevelData Node;

    public void VisitNode()
    {
        // Render();
    }

    public void Render()
    {
        _levelNameTxt.SetText($"{Node.LevelName}");
        foreach (var node in Node.RuntimeNodes)
        {
            var star= Instantiate(_starTemplate, _starContainer);
            if (node.IsVisited)
            {
                var starIcon = star.GetComponent<Image>();
                if (starIcon != null)
                    starIcon.sprite = _starFullIcon;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnVisitedNode?.Invoke(this);
    }
}
