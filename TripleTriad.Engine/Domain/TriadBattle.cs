using System;
using System.Linq;
using CSharpFunctionalExtensions;

namespace TripleTriad.Engine
{
    public enum BattleState 
    {
        START,
        PLAYER_TURN,
        OPPONENT_TURN,
        WIN,
        LOST
    }

    /// <summary>
    /// Une partie de Triple Triad  
    /// </summary>
    public class TriadBattle
    {
        public readonly Player player;
        public readonly Player opponent;
        public BattleState State { get; private set; }
        public Board Board { get; }
        public Action<BattleState> OnStateChanged { get; set; }

        public TriadBattle(Board board, Player player, Player opponent)
        {
            this.player = player;
            this.opponent = opponent;
            Board = board;
        }
        
        /// <summary>
        /// Transitionning Method
        /// </summary>
        /// <returns></returns>
        public Result AddCard(Card card, Position position)
        {
            if (Board.ContainsCard(position))
                return Result.Failure("Card already here");

            Board.DropCard(card, position);

            UpdatePlayerScore();
            UpdateBattleState();
            return Result.Success();
        }

        private void UpdatePlayerScore()
        {
            player.Score = GetScore(player);
            opponent.Score = GetScore(opponent);
        }

        private void UpdateBattleState()
        {
            if (IsFinished())
                State = (GetWinner() == player) ? BattleState.WIN : BattleState.LOST;
            else
            {
                State = (State == BattleState.PLAYER_TURN) 
                    ? BattleState.OPPONENT_TURN 
                    : BattleState.PLAYER_TURN;
            }
            OnStateChanged?.Invoke(State);
        }

        public Player GetWinner()
        {
            int playerResult = GetScore(player);
            int opponentResult = GetScore(opponent);

            return (playerResult > opponentResult)
                ? player
                : (playerResult < opponentResult)
                    ? opponent : null;
        }

        public bool IsFinished()
        {
            return Board.Slots.All(s => s.Value != null);
        }

        private int GetScore(Player player)
        {
            return Board.Slots.Count(s => s.Value != null 
                                          && s.Value.Card.CurrentOwner == player);
        }

        public void Start()
        {
            State = BattleState.PLAYER_TURN;
            // TODO - toss the first player
            OnStateChanged?.Invoke(State);
        }
    }
}
