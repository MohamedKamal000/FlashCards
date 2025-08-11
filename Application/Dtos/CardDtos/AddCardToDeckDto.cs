namespace Application.Dtos.CardDtos;

public class AddCardToDeckDto
{
    public string CardTitle { get; set; }
    
    public string Question { get; set; }
    
    public string Answer { get; set; }
    
    public string? PicturePath { get; set; }
}