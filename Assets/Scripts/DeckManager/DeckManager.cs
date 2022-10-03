using System;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private DeckData _deck;
    
    [SerializeField] private GameObject _cardTemplate;
    [SerializeField] private TMP_Text _cardCounter;

    private void Awake()
    {
        GenerateCardGrid(_deck);
    }

    public void GenerateCardGrid(DeckData deck)
    {
        _cardCounter.SetText($"{deck.AllCards.Count} / 30");
        foreach (CardData card in deck.AllCards)
        {
            var spawnedCard = Instantiate(_cardTemplate, new Vector2(0,0),
                 Quaternion.identity, this.transform);
            var card2dView = spawnedCard.GetComponent<Card2DView>();
            card2dView.SetCardController(card);
        }
    }
}
