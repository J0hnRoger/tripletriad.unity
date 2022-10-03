using System;
using System.Collections.Generic;

namespace TripleTriad.Infrastructure.Models
{

    [Serializable]
    public class DeckDto 
    {
        public string Name { get; set; } 
        public List<int> Cards { get; set; } 
    }
}