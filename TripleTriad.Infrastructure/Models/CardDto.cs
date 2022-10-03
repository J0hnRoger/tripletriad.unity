using System;

namespace TripleTriad.Infrastructure.Models
{
    [Serializable]
    public class CardDto
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
    }
}