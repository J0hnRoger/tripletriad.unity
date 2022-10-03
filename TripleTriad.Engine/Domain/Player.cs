using System.Collections.Generic;

namespace TripleTriad.Engine
{
    public class Player
    {
        public string Name { get; }
        public int Score { get; internal set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
    }

    public class DeckDomain
    {
        // TODO - logique de Shuffle/Draw/...
    }
}