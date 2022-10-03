using TripleTriad.Battle;
using TripleTriad.Engine;
using UnityEngine;

public class TestDropCardBehaviour : MonoBehaviour
{

    [SerializeField] private CardSlotBehaviour _slot;
    [SerializeField] private CardData _cardData;

    public void DropCard()
    {
        _slot.UpdateCard(_cardData, Color.green, true);
    }

    public void ResetSlot()
    {
        _slot.EmptySlot();
    }
}
