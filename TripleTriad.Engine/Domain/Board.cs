using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace TripleTriad.Engine
{
    public class Board
    {
        public Dictionary<Position, CardSlot> Slots { get; set; }

        public static Board CreateEmptyBoard()
        {
            return new Board()
            {
                Slots = new Dictionary<Position, CardSlot>()
                {
                    { Position.TopLeft, null },
                    { Position.Top, null },
                    { Position.TopRight, null },
                    { Position.MiddleLeft, null },
                    { Position.Middle, null },
                    { Position.MiddleRight, null },
                    { Position.BottomLeft, null },
                    { Position.Bottom, null },
                    { Position.BottomRight, null },
                }
            };
        }

        private void UpdateAdjacentSlots(Position position)
        {
            foreach (KeyValuePair<Position, CardSlot> slot in Slots)
            {
                if (slot.Value == null)
                    continue;
                slot.Value.IsCaptured = false;
            }

            List<Direction> comparisonDirection = GetComparisonDirection(position);
            var current = Slots[position];
            int currentIndex = Slots.Values.ToList().IndexOf(current);

            if (comparisonDirection.Contains(Direction.LEFT))
            {
                var leftCard =  currentIndex - 1 < 0 ? null : Slots.ElementAt(currentIndex - 1).Value;
                if (leftCard != null && leftCard.Card.Right < current.Card.Left)
                    leftCard.Capture(current.Card.CurrentOwner);
            }

            if (comparisonDirection.Contains(Direction.TOP))
            {
                var topCard = currentIndex - 3 < 0 ? null : Slots.ElementAt(currentIndex - 3).Value;
                if (topCard != null && topCard.Card.Bottom < current.Card.Top)
                    topCard.Capture(current.Card.CurrentOwner);
            }

            if (comparisonDirection.Contains(Direction.RIGHT))
            {
                var rightCard = currentIndex + 1 >= Slots.Count() ? null : Slots.ElementAt(currentIndex + 1).Value;
                if (rightCard != null && rightCard.Card.Left < current.Card.Right)
                    rightCard.Capture(current.Card.CurrentOwner);
            }

            if (comparisonDirection.Contains(Direction.BOTTOM))
            {
                var bottomCard = currentIndex + 3 >= Slots.Count() ? null : Slots.ElementAt(currentIndex + 3).Value;
                if (bottomCard != null && bottomCard.Card.Top < current.Card.Bottom)
                    bottomCard.Capture(current.Card.CurrentOwner);
            }
        }

        private List<Direction> GetComparisonDirection(Position position)
        {
            if (position == Position.TopLeft)
                return new List<Direction>() {Direction.RIGHT, Direction.BOTTOM };
            if (position == Position.Top)
                return new List<Direction>() {Direction.LEFT, Direction.RIGHT, Direction.BOTTOM };
            if (position == Position.TopRight)
                return new List<Direction>() { Direction.LEFT, Direction.BOTTOM };
            if (position == Position.MiddleLeft)
                return new List<Direction>() { Direction.RIGHT, Direction.TOP, Direction.BOTTOM };
            if (position == Position.Middle)
                return new List<Direction>() { Direction.LEFT, Direction.TOP, Direction.BOTTOM, Direction.RIGHT };
            if (position == Position.MiddleRight)
                return new List<Direction>() { Direction.LEFT, Direction.TOP, Direction.BOTTOM };
            if (position == Position.BottomLeft)
                return new List<Direction>() { Direction.TOP, Direction.RIGHT };
            if (position == Position.Bottom)
                return new List<Direction>() { Direction.LEFT, Direction.TOP, Direction.RIGHT };
            if (position == Position.BottomRight)
                return new List<Direction>() { Direction.LEFT, Direction.TOP };
            return null;
        }

        public bool ContainsCard(Position position)
        {
            return Slots[position] != null;
        }

        public void DropCard(Card card, Position position)
        {
            Slots[position] = new CardSlot(card);
            UpdateAdjacentSlots(position);     
        }

        public Result<Position> GetNextFreePosition()
        {
            foreach (var pos in Slots)
                if (!ContainsCard(pos.Key))
                    return Result.Success<Position>(pos.Key);
            return Result.Failure<Position>("No slot available");
        }
    }

    internal enum Direction
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT
    }
}