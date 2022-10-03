using System;
using System.Collections.Generic;

namespace TripleTriad.Infrastructure.Models
{
    public class PlayerDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public DeckDto Deck { get; set; } 
        public string LevelName { get; set; }
        public int QuestId { get; set; }
    }
}
