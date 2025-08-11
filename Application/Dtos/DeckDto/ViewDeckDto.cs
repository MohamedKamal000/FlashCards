namespace Application.Dtos.DeckDto;

public class ViewDeckDto
{
    public string DeckId { get; set; }
    
    public string DeckName { get; set; }
    
    public string CreatorId { get; set; }
    
    public string DeckDescription { get; set; }
    
    public bool IsPublic { get; set; }
}