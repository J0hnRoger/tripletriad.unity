using TripleTriad.Engine;

namespace TripleTriad.Battle
{
    public class CardController
    {
        public CardData CardData;
        public Card Card { get; set; }

        public CardController(CardData cardData, Player owner)
        {
            CardData = cardData;
            Card = new Card(owner, cardData.Top, cardData.Right, cardData.Bottom, cardData.Left, cardData.Name);
            Card.CardId = cardData.Id;
        }

        public CardController(CardData cardData)
        {
            CardData = cardData;
        }
    }
}
