using System.Linq;
using TMPro;
using TripleTriad.Engine;
using UnityEngine;

namespace TripleTriad.Battle
{

    public class CardView : MonoBehaviour
    {
        [HideInInspector] public int handIndex;

        [SerializeField] public CardController Card;

        private void Start()
        {
            Render();
        }

        public void Render()
        {
            var faceRenderer = GetComponentsInChildren<SpriteRenderer>().First(s => s.gameObject.name == "SpriteFace");
            faceRenderer.sprite = Card.CardData.ArtWork;
            // faceRenderer.transform.localScale = faceRenderer.sprite.rect.size * faceRenderer.sprite.pixelsPerUnit; 
            MapValues();
        }

        private void MapValues()
        {
            Card card = Card.Card;   
            GetComponentsInChildren<TMP_Text>().ToList()
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
                    if(tmp.gameObject.name.Contains("Name"))
                        tmp.SetText(card.Name);
                });
        }
    }
}
