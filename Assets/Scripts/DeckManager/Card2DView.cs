using TripleTriad.Battle;
using TripleTriad.Engine;
using UnityEngine;
using UnityEngine.UI;

public class Card2DView : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    } 

    public void SetCardController(CardData card)
    {
        _image.sprite = card.ArtWork;
    }
}
