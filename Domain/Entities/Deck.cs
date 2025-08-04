namespace Domain.Entities;

public class Deck
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string DeckName { get; set; }
    public string DeckDescription { get; set; }
    public Ulid CreatorId { get; set; }
    public bool IsPublic { get; set; }
    public User CreatorUser { get; set; }
    public bool IsDeletedByCreator { get; set; }
    public Category DeckCategory { get; set; }
    public ICollection<User> ReferencedUsers { get; set; }
    public ICollection<Card> Cards { get; set; }
}