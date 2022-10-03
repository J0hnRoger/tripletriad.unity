using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using UnityEngine;

/// <summary>
/// List all cards in the game
/// </summary>
[CreateAssetMenu(menuName = "TripleTriad/Library", fileName = "New Library")]
public class CardsList : ScriptableObject
{
    public List<CardData> Items;
    void OnEnable()
    {
        #if UNITY_EDITOR
            Items = ScriptableObjectUtils.GetAllInstances<CardData>()
                .ToList();
        #endif
    }
}
