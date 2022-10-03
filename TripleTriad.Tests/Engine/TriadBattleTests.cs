
using FluentAssertions;
using Xunit;

namespace TripleTriad.Engine.Tests
{
    public class TriadBattleTests
    {
        [Fact]
        public void TriadBattle_Init_EmptyBoard()
        {
            var newBattle = CreateBattleWithEmptyBoard();
            var card = new Card(null, 1,1,1,1);

            var result = newBattle.AddCard(card, Position.Middle);
            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public void TriadBattle_ReturnFailure_WhenCardAlreadyHere()
        {
            var newBattle = CreateBattleWithEmptyBoard();
            var card = new Card(null, 1,1,1,1);

            var result = newBattle.AddCard(card, Position.Middle);
            result.IsSuccess.Should().Be(true);

            result = newBattle.AddCard(card, Position.Middle);
            result.IsSuccess.Should().BeFalse();
        }
        

        private static TriadBattle CreateBattleWithEmptyBoard()
        {
            var newBattle = new TriadBattle(Board.CreateEmptyBoard(), new Player("John"), new Player("Enemy"));
            return newBattle;
        }

        [Fact]
        public void TriadBattle_ReturnBoard_WithActivePlayer()
        {
            var newBattle = CreateBattleWithEmptyBoard();
            var activePlayer = newBattle.player; 
            var card = new Card(activePlayer, 1, 1, 1, 1);

            var result = newBattle.AddCard(card, Position.Middle);
            result.IsSuccess.Should().Be(true);

            result = newBattle.AddCard(card, Position.Middle);
            result.IsSuccess.Should().BeFalse();
            newBattle.Board.Slots[Position.Middle].Card.CurrentOwner.Should().Be(activePlayer);
        }

        [Fact]
        public void TriadBattle_ReturnBoard_WithCapturedCardOfTheTurn()
        {
            var newBattle = CreateBattleWithEmptyBoard();
            var activePlayer = newBattle.player;
            var opponent = newBattle.opponent;
            var card = new Card( activePlayer, 1, 1, 1, 1);
            var opponentCard = new Card( opponent, 1, 2, 1, 1);

            var result = newBattle.AddCard(card, Position.Middle);
            result.IsSuccess.Should().Be(true);

            newBattle.AddCard(opponentCard, Position.MiddleLeft);
            newBattle.Board.Slots[Position.Middle].IsCaptured.Should().Be(true);
            newBattle.Board.Slots[Position.Middle].Card.CurrentOwner.Should().Be(opponent);
        }

        [Fact]
        public void TriadBattle_ReturnBoard_WithCardNotCapturedInTheTurn()
        {
            var newBattle = CreateBattleWithEmptyBoard();
            var activePlayer = newBattle.player;
            var opponent = newBattle.opponent;
            var capturedCard = new Card( activePlayer, 1, 1, 1, 1);
            var notCaptured = new Card( activePlayer, 3, 1, 1, 1);
            var idleCard = new Card( activePlayer, 1, 1, 1, 1);
            var opponentCard = new Card( opponent, 1, 2, 1, 1);

            newBattle.AddCard(capturedCard, Position.Middle);
            newBattle.AddCard(notCaptured, Position.BottomLeft);
            newBattle.AddCard(idleCard, Position.TopRight);
            newBattle.AddCard(opponentCard, Position.MiddleLeft);
            newBattle.Board.Slots[Position.BottomLeft].IsCaptured.Should().Be(false);
            newBattle.Board.Slots[Position.Middle].IsCaptured.Should().Be(true);
            newBattle.Board.Slots[Position.Middle].Card.CurrentOwner.Should().Be(opponent);
        }

        [Fact]
        public void TriadBattle_ReturnScore()
        {
            var newBattle = CreateBattleWithEmptyBoard();
            var activePlayer = newBattle.player;
            newBattle.player.Score.Should().Be(0);

            var card = new Card( activePlayer, 1, 1, 1, 1);
            newBattle.AddCard(card, Position.Bottom);

            newBattle.player.Score.Should().Be(1);
        }

        [Fact]
        public void BoardCaptureCardAtTop()
        {
            var newBattle = CreateBattleWithEmptyBoard();
            var activePlayer = newBattle.player;
            var opponent = newBattle.opponent;
            var capturedCard = new Card(activePlayer, 1, 4, 7, 1);
            var card = new Card( opponent, 4, 2, 7, 4);

            newBattle.AddCard(capturedCard, Position.MiddleRight);
            newBattle.AddCard(card, Position.BottomRight);
            
            newBattle.Board.Slots[Position.MiddleRight].IsCaptured.Should().Be(false);
            }

        [Fact]
        public void Board_DoesntCapture_CardOnThePreviousLine()
        {
            var newBattle = CreateBattleWithEmptyBoard();
            var activePlayer = newBattle.player;
            var opponent = newBattle.opponent;
            var notCapturedCard = new Card(activePlayer, 1, 3, 7, 1);
            var card = new Card( opponent, 4, 2, 7, 4);

            newBattle.AddCard(notCapturedCard, Position.MiddleRight);
            newBattle.AddCard(card, Position.BottomLeft);
            
            newBattle.Board.Slots[Position.MiddleRight].IsCaptured.Should().Be(false);
        }

        [Fact]
        public void Battle_GameLoop()
        {
            var newBattle = CreateBattleWithEmptyBoard();
            newBattle.Start();
            newBattle.State.Should().Be(BattleState.PLAYER_TURN);

            newBattle.AddCard(new Card(newBattle.player, 1,1,1,1, "testCard"), Position.Bottom);
            newBattle.State.Should().Be(BattleState.OPPONENT_TURN);

            newBattle.AddCard(new Card(newBattle.opponent, 1,1,1,1, "testCard"), Position.MiddleRight);
            newBattle.State.Should().Be(BattleState.PLAYER_TURN);

            while (!newBattle.IsFinished())
            {
                var currentPlayer = (newBattle.State == BattleState.PLAYER_TURN)
                    ? newBattle.player
                    : newBattle.opponent;
                Card template = new Card(currentPlayer, 1,1,1,1, "testCard");
                var nextFreePosition = newBattle.Board.GetNextFreePosition();
                if (nextFreePosition.IsFailure)
                    break;
                newBattle.AddCard(template, nextFreePosition.Value);
            }
            newBattle.State.Should().Be(BattleState.WIN);
        }
    }
}
