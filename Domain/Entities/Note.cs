namespace Domain.Entities;

public class Note
{
    public Ulid Id { get; set; } = Ulid.NewUlid();

    public Ulid UserId { get; set; }

    public User User { get; set; }

    public string NoteFilePath { get; set; }
}