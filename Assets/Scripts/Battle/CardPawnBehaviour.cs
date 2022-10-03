using System.Linq;
using TMPro;
using UnityEngine;

namespace TripleTriad.Battle {

    public class CardPawnBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _thumbnailRenderer;
        [SerializeField] private SpriteRenderer _colorRenderer;
        private Animator _animator;

        public void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetCardSprite(Sprite cardSprite, Color cardColor, bool isCaptured)
        {
            var parent = _thumbnailRenderer.gameObject.transform;
            _thumbnailRenderer.sprite = cardSprite;
            _thumbnailRenderer.color = Color.white;
            _colorRenderer.color = cardColor;
            if (isCaptured)
                _animator.SetTrigger("taken");
        }
    
        public void MapValues(CardData card)
        {
            gameObject.GetComponentsInChildren<TMP_Text>().ToList()
                .ForEach(tmp =>
                {
                    if (tmp.gameObject.name.Contains("Left"))
                        tmp.SetText(card.Left.ToString());
                    if (tmp.gameObject.name.Contains("Right"))
                        tmp.SetText(card.Right.ToString());
                    if (tmp.gameObject.name.Contains("Top"))
                        tmp.SetText(card.Top.ToString());
                    if (tmp.gameObject.name.Contains("Bot"))
                        tmp.SetText(card.Bottom.ToString());
                });
        }

    }
}

