namespace Application.Dtos.DeckDto;

public class ChangeDeckDto
{
    public string DeckId { get; set; }
    
    public string DeckName { get; set; }
    
    public string DeckDescription { get; set; }
    
    public bool IsPublic { get; set; }

}