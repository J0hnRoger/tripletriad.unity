namespace TripleTriad.Engine
{
    public class Card
    {
        public int CardId { get; set; }
        public Player Owner { get; internal set; }
        public Player CurrentOwner { get; internal set; }
        public string Name { get; internal set; }
        public int Top { get; internal set; }
        public int Left { get; internal set; }
        public int Right { get; internal set; }
        public int Bottom { get; internal set; }

        public Card(){}
        public Card(Player owner, int top, int right, int bottom, int left, string name = "Test Card")
        {
            Owner = owner;
            Name = name;
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
            CurrentOwner = owner;
        }

        public void SetOwner(Player owner)
        {
            CurrentOwner = owner;
        }
    }
}