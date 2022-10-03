using System;
using TripleTriad.Core.SOs;
using TripleTriad.Engine;
using UnityEngine;

namespace TripleTriad.Battle
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] public Color PlayerColor;
        [SerializeField] private StringReference _playerName;
        [SerializeField] private DeckData _deckData;
    
        [HideInInspector] public Player CurrentPlayer;
        [HideInInspector] public Deck Deck;

        private IDrawBehaviour drawBehaviour; 

        private void Awake()
        {
            SetPlayer(_playerName.Value, _deckData);
            drawBehaviour = GetComponent<IDrawBehaviour>();
        }

        public void SetPlayer(string playerName, DeckData deckData)
        {
            if (!String.IsNullOrEmpty(playerName))
                CurrentPlayer = new Player(playerName);
            if (deckData != null)
                Deck = new Deck(deckData, CurrentPlayer);
        }

        public void ShuffleCards()
        {
            Deck.ShuffleCards();
        }

        public void DrawHand()
        {
            drawBehaviour.DrawHand(Deck);
        }

        public void DrawCard()
        {
            drawBehaviour.DrawCard(Deck);
        }

        public void FreeSlot(int currentCardHandIndex)
        {
            drawBehaviour.FreeSlot(currentCardHandIndex);
        }

        public Vector3 GetPositionOfHand(int currentCardHandIndex)
        {
            return drawBehaviour.GetPositionOfHand(currentCardHandIndex);
        }
    }
}
