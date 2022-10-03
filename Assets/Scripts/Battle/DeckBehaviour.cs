using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using TripleTriad.Engine;

namespace TripleTriad.Battle
{
    /// <summary>
    /// Deck utilisé durant une partie
    /// </summary>
    public class Deck 
    {
        private DeckData _deckData;
        private readonly Player _owner;
        private Random _rng = new();

        public Stack<CardController> Cards { get; private set; } = new();

        public Deck(DeckData data, Player owner)
        {
            _deckData = data;
            _owner = owner;
            ResetCards();
        }

        public void ShuffleCards()
        {
            ResetCards();
        }

        private void ResetCards()
        {
            Cards = new Stack<CardController>();
            _deckData.Reset();
            foreach (var dataDeckCard in _deckData.DeckCards)
                Cards.Push(new CardController(dataDeckCard, _owner)); 
        }

        public void AddCard(CardData wonCard)
        {
            Cards.Push(new CardController(wonCard, _owner));
            _deckData.AddCard(wonCard);
        } 

        public Result<CardController> GetNextCardFromDeck()
        {
            if (Cards.Count == 0)
                return Result.Failure<CardController>("Plus de cartes dans le deck");

            var currentCard = Cards.Pop();
            return Result.Success(currentCard);
        }

    }
}
