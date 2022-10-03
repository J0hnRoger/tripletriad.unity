using UnityEngine;

[CreateAssetMenu(menuName = "TripleTriad/New Card", fileName = "New Card")]
public class CardData : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite ArtWork;
    public Sprite Thumbnail;

    public int Top;
    public int Right;
    public int Bottom;
    public int Left;

    public int GetCardScore()
    {
        return Top + Bottom + Left + Right;
    }

    public void Set(CardData clonedCard)
    {
        Id = clonedCard.Id;
        
        Name = clonedCard.Name;
        Thumbnail = clonedCard.Thumbnail;
        ArtWork = clonedCard.ArtWork;
        
        Bottom = clonedCard.Bottom;
        Top = clonedCard.Top;
        Left = clonedCard.Left;
        Right = clonedCard.Right;
    }
}
