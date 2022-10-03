using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "NewDeck", menuName = "TripleTriad/New Deck")]
public class DeckData : ScriptableObject
{
    [SerializeField] private List<CardData> _cards = new(); 
    [NonSerialized] public Stack<CardData> DeckCards;

    public List<CardData> AllCards => _cards;
    private Random _rng = new();

    public void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        DeckCards = new Stack<CardData>();
        foreach (CardData c in Shuffle(_cards))
            DeckCards.Push(c);
    }

    private List<CardData> Shuffle(List<CardData> cards)
    {
        // Fisher-Yates Algo
        int count = cards.Count;
        while (count > 1)
        {
            count--;
            int randomIdx = _rng.Next(count + 1);
            var current = cards[randomIdx];
            cards[randomIdx] = cards[count];
            cards[count] = current;
        }
        return cards;
    }

    public void Init(List<CardData> cards)
    {
        _cards = new List<CardData>(cards);
        DeckCards = new Stack<CardData>(); 
        Reset();
    }

    public void AddCard(CardData wonCard)
    {   
        _cards.Add(wonCard);
        Reset();
    }

    public void InitFrom(DeckData starterDeck)
    {
        Init(starterDeck._cards);
    }
    
    public CardData GetStrongestCard()
    {
        return _cards.Aggregate((c1,c2) => c1.GetCardScore() > c2.GetCardScore() ? c1 : c2);
    }
}