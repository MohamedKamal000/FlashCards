namespace Application.Dtos.UserDtos;

public class ChangeUserDetailsDto
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }

    public string? Bio { get; set; }

    public string? PicturePath { get; set; }
}