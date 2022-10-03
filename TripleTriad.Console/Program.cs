using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;
using TripleTriad.Engine;

namespace TripleTriad.Console
{
    internal class Program
    {
        private static Player currentPlayer; 
        private static Player opponent; 
        
        public static void Main(string[] args)
        {
            var playerName = AnsiConsole.Ask<string>("What's your [green]name[/]?");
            currentPlayer = new Player(playerName); 
            opponent = new Player("Opponent");

            var playerDeck = GetDeck(currentPlayer); 
            var opponentDeck = GetDeck(opponent); 
            
            var battle = new TriadBattle(Board.CreateEmptyBoard(), currentPlayer, opponent);
            while (!battle.IsFinished())
            {
                DisplayPlayerHand(playerDeck);
                var playingCard = ChooseCard(playerDeck);
                var position = ChoosePosition();
                battle.AddCard(playingCard, position);
                DisplayBoard(battle.Board);
                var opponentCard = EnemyChooseCard();
                var opponentPosition = battle.Board.GetNextFreePosition();
                if (opponentPosition.IsSuccess)
                {
                    battle.AddCard(opponentCard, opponentPosition.Value);
                    AnsiConsole.Write(new Markup($"[red]Opponent played {opponentCard.Name} on {opponentPosition.Value.ToString()} [/]"));
                    AnsiConsole.WriteLine();
                    DisplayBoard(battle.Board);
                }
            }

            string endMessage = battle.GetWinner() == currentPlayer
                ? "[green] Congrats! you win.[/]"
                : "[red] You loose.[/]";
            AnsiConsole.MarkupLine($"[bold]{endMessage} - score: {currentPlayer.Score} - {opponent.Score}[/]");
        }

        private static void DisplayScore()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold underline on blue][yellow]{currentPlayer.Score} - {opponent.Score}[/][/]");
        }

        private static Position ChoosePosition()
        {
            var position = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose the position")
                .PageSize(9)
                .AddChoices(new[] {
                    "TopLeft", "Top", "TopRight",
                    "MiddleLeft", "Middle", "MiddleRight",
                    "BottomLeft", "Bottom", "BottomRight",
                }));
            
            Enum.TryParse(position, out Position pos);
            return pos;
        }

        private static List<Card> GetDeck(Player player)
        {
            return new List<Card>()
            {
                new Card(player, 2, 3, 2, 1, "Cat"),
                new Card(player, 1, 1, 1, 1, "Chicken"),
                new Card(player, 4, 1, 1, 1, "Mouse"),
                new Card(player, 2, 2, 2, 2, "Pig"),
                new Card(player, 1, 1, 1, 9, "Fish"),
            };
        }

        private static Card EnemyChooseCard()
        {
            return new Card(null, 1, 1, 1, 1, "Poulet");
        }

        private static void DisplayBoard(Board battleBoard)
        {
            var boardTable = new Table();
            boardTable.AddColumn("");
            boardTable.AddColumn("");
            boardTable.AddColumn("");
            boardTable.HideHeaders();

            for (int i = 0; i < 3; i++)
            {
                boardTable.AddRow(battleBoard.Slots
                    .Skip(i*3) 
                    .Take(3)
                    .Select(cs => DisplayCard(cs.Value?.Card)));
            }
            AnsiConsole.Write(new Markup("[bold][green]Board Status[/][/]"));
            DisplayScore();
            AnsiConsole.Write(boardTable);
        }

        private static Card ChooseCard(List<Card> deck)
        {
            int index = AnsiConsole.Ask<int>("Choose a Card");
            return deck[index];
        }

        private static void DisplayPlayerHand(List<Card> playerCards)
        {
            var table = new Table();
            for (var i = 0; i < playerCards.Count; i++)
                table.AddColumn($"({i})");
           
            table.AddRow(playerCards.Select(DisplayCard)); 
            AnsiConsole.Write(table);
        }

        private static Table DisplayCard(Card card)
        {
            if (card == null)
                return new Table();
            Color colorCode = card.CurrentOwner == currentPlayer 
                ? Color.Green 
                : Color.Red;
            
            var table = new Table();
            table.AddColumn("").BorderColor(colorCode); 
            table.AddColumn(""); 
            table.AddColumn("");
            table.HideHeaders();
            
            table.AddRow("", card.Top.ToString(), "");
            table.AddRow(card.Left.ToString(), card.Name, card.Right.ToString());
            table.AddRow("", card.Bottom.ToString(), "");
            return table;
        }
    }
}