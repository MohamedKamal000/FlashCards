namespace Application.Dtos.UserDtos;

public class RegisterUserDto
{
    public string Name { get; set; }
    
    public string Email { get; set; }

    public string Password { get; set; }

    public string? Bio { get; set; }

    public string? PicturePath { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(Name)}: {Name}, {nameof(Email)}: {Email}, {nameof(Password)}: {Password}, {nameof(Bio)}: {Bio}, {nameof(PicturePath)}: {PicturePath}";
    }
}