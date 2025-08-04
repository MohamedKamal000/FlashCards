namespace Domain.Entities;

public class Card
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    
    public string CardTitle { get; set; }
    
    public string Question { get; set; }
    
    public string Answer { get; set; }
    
    public Ulid DeckId { get; set; }
    
    public string? PicturePath { get; set; }
    
    public Deck DeckUsedIn { get; set; }
}