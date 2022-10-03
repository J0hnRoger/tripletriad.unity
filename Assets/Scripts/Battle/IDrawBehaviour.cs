using CSharpFunctionalExtensions;
using UnityEngine;

namespace TripleTriad.Battle
{
    public interface IDrawBehaviour
    {
        Result<CardController> DrawCard(Deck deck);
        void FreeSlot(int handIndex);
        void DrawHand(Deck deck);
        Vector3 GetPositionOfHand(int currentCardHandIndex);
    }
}
