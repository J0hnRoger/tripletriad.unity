using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TripleTriad.Core.EventArchitecture.Events;
using TripleTriad.Core.SOs;
using TripleTriad.Engine;
using UnityEngine;

namespace TripleTriad.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BoardView _boardView;
        [SerializeField] private InfoBarView _infoBarView;
        [SerializeField] private EndResultView _endResultView;

        [SerializeField] private FloatReference _IAPlayingTime;

        [SerializeField] private BoolEvent _onBattleEndEvent; 

        [SerializeField] private IntEvent _onPlayerScoreChanged;
        [SerializeField] private IntEvent _onOpponentScoreChanged;
        
        public static Action<BattleState, PlayerBehaviour> OnTurnChangedEvent;

        public CardsList AllCards;

        [SerializeField] public MapNodeData _currentBattle;
        
        [SerializeField] public PlayerBehaviour Player;
        [SerializeField] public PlayerBehaviour Opponent;

        public bool PlayerTurn => _playerTurn;
        private bool _playerTurn = true;

        private TriadBattle _battle;

        private void OnEnable()
        {
            DragCardsManager.OnCardRelease += DropCard;
        }

        private void OnDisable()
        {
            DragCardsManager.OnCardRelease -= DropCard;
        }

        private void Start()
        {
           Opponent.SetPlayer(_currentBattle.Opponent.Name, _currentBattle.Opponent.Deck); 
           InitBattle();
        }

        public void InitBattle()
        {
            Player.ShuffleCards();
            Opponent.ShuffleCards();

            // Delegation au Domain 
            _battle = new TriadBattle(Board.CreateEmptyBoard(), Player.CurrentPlayer, Opponent.CurrentPlayer);
            // On communique avec le Domain avec des Events pour ne pas coupler les 2 
            _battle.OnStateChanged += OnStateChanged;

            _battle.Start();

            Player.DrawHand();

            _onPlayerScoreChanged?.RaiseEvent(0);
            _onOpponentScoreChanged?.RaiseEvent(0);

            RenderBoard();
        }

        private void OnStateChanged(BattleState state)
        {
            var currentPlayer = (state == BattleState.PLAYER_TURN)
                ? Player
                : Opponent;
             
            OnTurnChangedEvent?.Invoke(state, currentPlayer);
        }

        public void EndBattle()
        {
            Player winner = _battle.GetWinner();
        
            string message = (winner == Player.CurrentPlayer)
                ? "Bien jou?! Rejouer"
                : "Perdu! Rejouer";

            _infoBarView.SetActionableMessage(message);
            

            if (winner == null)
            {
                _infoBarView.SetActionableMessage("Egalit?! Rejouer");
                return;
            }

            _endResultView.SetResult(true, 
                () =>
                {
                    _onBattleEndEvent?.RaiseEvent(winner == Player.CurrentPlayer);
                });
        }

        // Pose une carte sur le Board
        public void DropCard(CardController droppedCard, Position position)
        {
            Card currentCard = droppedCard.Card;

            _battle.AddCard(currentCard, position);

            RenderBoard();

            // notify Score
            _onOpponentScoreChanged?.RaiseEvent(_battle.opponent.Score);
            _onPlayerScoreChanged?.RaiseEvent(_battle.player.Score);

            if (_battle.IsFinished())
            {
                EndBattle();
                return;
            }

            // change Active player
            _playerTurn = _battle.State == BattleState.PLAYER_TURN;

            if (!_playerTurn)
                StartCoroutine(IAPlay());
        }

        private void RenderBoard()
        {
            // Todo - keep only the changed _cards
            foreach (KeyValuePair<Position, CardSlot> newPos in _battle.Board.Slots)
            {
                if (newPos.Value == null)
                {
                    _boardView.CardSlots[newPos.Key].EmptySlot();
                    continue;
                }

                Card currentCard = newPos.Value.Card;
                Color color = currentCard.CurrentOwner == Player.CurrentPlayer
                    ? Player.PlayerColor
                    : Opponent.PlayerColor;
                CardData currentCardData = AllCards.Items.First(c => c.Id == currentCard.CardId);
                _boardView.CardSlots[newPos.Key]
                    .UpdateCard(currentCardData, color, newPos.Value.IsCaptured);
            }
        }

        public IEnumerator IAPlay()
        {
            var cardResult = Opponent.Deck.GetNextCardFromDeck();
            var freePosResult = _battle.Board.GetNextFreePosition();
            if (freePosResult.IsFailure)
                yield return null;

            yield return new WaitForSecondsRealtime(_IAPlayingTime);

            if (cardResult.IsSuccess)
            {
                DropCard(cardResult.Value, freePosResult.Value);
                Player.DrawCard();
                yield return null;
            }
        }
    }
}
