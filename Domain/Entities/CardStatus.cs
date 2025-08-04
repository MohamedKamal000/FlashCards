namespace Domain.Entities;

public class CardStatus
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    
    public Ulid UserId { get; set; }
    
    public Ulid CardId { get; set; }
    
    public bool NeedsRevision { get; set; }
    
    public User User { get; set; }
    
    public Card Card { get; set; }
}