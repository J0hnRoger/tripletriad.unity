using System;
using TripleTriad.Engine;
using TMPro;
using TripleTriad.Battle;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{
    [SerializeField] private CardData _wonCard;
    
    [SerializeField] private GameObject _cardGO;
    [SerializeField] private TMP_Text _cardTitle;

    void Awake()
    {
        _cardGO.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        CardView cardView = _cardGO.GetComponentInChildren<CardView>();
        cardView.Card = new CardController(_wonCard, null); 
        cardView.Render();
        _cardTitle.SetText($"La carte {_wonCard.Name} rejoint votre armée");
        _cardGO.gameObject.SetActive(true);
    }
}
