using Domain.Entities;

namespace Application.Dtos.DeckDto;

public class CreateDeckDto
{
    public string UserId { get; set; }
    
    public string DeckName { get; set; }
    
    public string DeckDescription { get; set; }

    public bool IsPublic { get; set; }

    public Category DeckCategory { get; set; }
}