namespace Domain.Entities;

public class User
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string? Bio { get; set; }

    public string? PicturePath { get; set; }
    public ICollection<Deck> ReferencedDecks { get; set; }
    public ICollection<Deck> CreatedDecks { get; set; }
    public ICollection<Note> Notes { get; set; }
    public ICollection<CardStatus> RevisionList { get; set; }
}