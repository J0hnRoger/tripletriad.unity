using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewPlayer", menuName = "TripleTriad/New Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField] public DeckData Deck;
    [SerializeField] public string Name;
}
