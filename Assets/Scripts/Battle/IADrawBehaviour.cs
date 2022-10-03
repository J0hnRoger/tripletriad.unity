using CSharpFunctionalExtensions;
using UnityEngine;

namespace TripleTriad.Battle
{
    public class IADrawBehaviour : MonoBehaviour, IDrawBehaviour
    {
        public Result<CardController> DrawCard(Deck deck)
        {
            return new CardController(deck.Cards.Pop().CardData);
        }

        public void FreeSlot(int handIndex)
        {
        }

        public void DrawHand(Deck deck)
        {
        }

        public Vector3 GetPositionOfHand(int currentCardHandIndex)
        {
            return Vector3.one;
        }
    }
}
