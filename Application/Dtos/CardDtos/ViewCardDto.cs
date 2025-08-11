namespace Application.Dtos.CardDtos;

public class ViewCardDto
{
    public string Id { get; set; }
    
    public string CardTitle { get; set; }
    
    public string Question { get; set; }
    
    public string Answer { get; set; }
    
    public string? PicturePath { get; set; }
}