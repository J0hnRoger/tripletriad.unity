using CSharpFunctionalExtensions;
using UnityEngine;

namespace TripleTriad.Battle
{
    public class PlayerDrawBehaviour : MonoBehaviour, IDrawBehaviour
    {
        [SerializeField] private GameObject _cardTemplate;
        [SerializeField] public Transform[] cardSlots;

        [HideInInspector] public bool[] availableCardSlots;
        public CardView[] occupedCardSlots;

        private void Awake()
        {
            availableCardSlots = new bool[cardSlots.Length];
            occupedCardSlots = new CardView[cardSlots.Length];
            for (int i = 0; i < cardSlots.Length; i++)
                FreeSlot(i);
        }

        public Result<CardController> DrawCard(Deck deck)
        {
            Result<CardController> randomCardResult = deck.GetNextCardFromDeck();
            if (randomCardResult.IsFailure)
                return Result.Failure<CardController>(randomCardResult.Error);

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    GameObject drawnCard = Instantiate(_cardTemplate, cardSlots[i].position,
                        Quaternion.identity, cardSlots[i]);

                    var cardView = drawnCard.GetComponentInChildren<CardView>();
                    cardView.Card = randomCardResult.Value;
                    cardView.handIndex = i;

                    cardView.Render();

                    occupedCardSlots[i] = cardView;
                    availableCardSlots[i] = false;
                    return randomCardResult.Value;
                }
            }
            return randomCardResult.Value;
        }

        public void FreeSlot(int handIndex)
        {
            availableCardSlots[handIndex] = true;
        }

        public void DrawHand(Deck deck)
        {
            for (int i = 0; i < cardSlots.Length; i++)
            {
                if (occupedCardSlots[i] != null)
                    Destroy(occupedCardSlots[i].gameObject);
                availableCardSlots[i] = true;
            }

            foreach (var slot in availableCardSlots)
                DrawCard(deck);
        }

        public Vector3 GetPositionOfHand(int currentCardHandIndex)
        {
            return cardSlots[currentCardHandIndex].transform.position;
        }
    }
}
