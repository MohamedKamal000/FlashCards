namespace Application.Dtos.UserDtos;

public class ChangePasswordDto
{
    public string UserId;
    
    public string OldPassword { get; set; }
    
    public string NewPassword { get; set; }
}