using TripleTriad.Engine;
using UnityEngine;

namespace TripleTriad.Battle
{
    public class CardSlotBehaviour : MonoBehaviour
    {
        [SerializeField] private Sprite _hoveringSprite;
        [SerializeField] private GameObject _cardPawnTemplate;
        [SerializeField] private Transform _parent;

        public Position Position;
    
        private bool _isEmpty;
        private CardPawnBehaviour currentPawn;

        public bool IsEmpty => _isEmpty;

        private void Awake()
        {
            _isEmpty = true;
            _cardPawnTemplate.SetActive(false);
        }

        public void EmptySlot()
        {
            if (currentPawn != null)
                Destroy(currentPawn.gameObject);
            _isEmpty = true;
        }

        public bool UpdateCard(CardData card, Color ownerColor, bool isCaptured)
        {
            if (!_isEmpty)
            {
                if (isCaptured)
                    currentPawn.SetCardSprite(card.Thumbnail, ownerColor, isCaptured);
                return false;
            }

            // TODO - idealement j'aurais voulu placer le pion en child de la case, mais j'ai des problèmes de scaling
            GameObject pawn = Instantiate(_cardPawnTemplate, transform.position,
                Quaternion.identity, _parent);
            pawn.SetActive(true);
        
            currentPawn = pawn.GetComponent<CardPawnBehaviour>();
            currentPawn.SetCardSprite(card.Thumbnail, ownerColor, isCaptured);
            currentPawn.MapValues(card);

            _isEmpty = false;
            return true;
        }
    }
}
