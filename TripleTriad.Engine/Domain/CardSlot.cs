namespace TripleTriad.Engine
{
    public class CardSlot
    {
        public Card Card { get; set;} 
        public bool IsCaptured { get; set;} 

        public CardSlot(Card card)
        {
            Card = card;
        }

        public void Capture(Player capturer)
        {
            Card.CurrentOwner = capturer;
            IsCaptured = true;
        }

    }
}