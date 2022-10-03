using System;
using System.Collections.Generic;
using System.Linq;
using TripleTriad.Engine;
using UnityEngine;

namespace TripleTriad.Battle
{
    public class BoardView : MonoBehaviour
    {
        public Action<Card, Position> OnCardDropped;

        public Dictionary<Position, CardSlotBehaviour> CardSlots;
        // Start is called before the first frame update
        public void Awake()
        {
            // get _cards 
            CardSlots = GetComponentsInChildren<CardSlotBehaviour>()
                .ToDictionary(k => k.Position, c => c);
        }
    }
}
