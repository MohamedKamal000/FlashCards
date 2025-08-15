using Application.Dtos.UserDtos;
using Application.Respondbodies;
using Application.Utilities;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AuthService
{
    private IUnitOfWork _unitOfWork { get; }
    private IPasswordHasher PasswordHasher { get ; }
    
    public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        PasswordHasher = passwordHasher;
    }

    public async Task<Response<TokenBodyResponse>> RegisterUser(RegisterUserDto userDto)
    {
        // validation happens here using fluent validation
        var user = EntityMapper.ProjectDtoToEntity<RegisterUserDto,User>(userDto);

        user.Password = PasswordHasher.Hash(user.Password);

        await _unitOfWork.UserRepository.Add(user);

        await _unitOfWork.SaveChanges();

        var token = new TokenBodyResponse()
        {
            AccessToken = user.Id.ToString(),
            RefreshToken = user.Id.ToString()
        };
        return Response<TokenBodyResponse>.GetAcceptedRequest("User Created", token);
    }

    public async Task<Response<TokenBodyResponse>> LoginUser(LoginUserDto loginUserDto)
    {
        var user = await 
            _unitOfWork.UserRepository.Find(u => u.Email == loginUserDto.Email);
        if (user is null)
            return Response<TokenBodyResponse>.GetNotAcceptedRequest("Email or Password is not correct");
        
        if (!PasswordHasher.Verify(user.Password,loginUserDto.Password))
            return Response<TokenBodyResponse>.GetNotAcceptedRequest("Email or Password is not correct");


        return Response<TokenBodyResponse>.GetAcceptedRequest("Successfully logged in !", null);
    }


    public async Task<Response<TokenBodyResponse>> ChangePassword
        (ChangePasswordDto changePasswordDto)
    {
        if (!Ulid.TryParse(changePasswordDto.UserId, out var ulid))
        {
            return Response<TokenBodyResponse>.GetNotAcceptedRequest("Invalid user ID format");
        }
    

        var user = await _unitOfWork.UserRepository.Find(u => u.Id == ulid);
        
        if (user is null)
            return Response<TokenBodyResponse>.GetNotAcceptedRequest("Email or Password is not correct");
        
        if (!PasswordHasher.Verify(user.Password,changePasswordDto.OldPassword))
            return Response<TokenBodyResponse>.GetNotAcceptedRequest("Email or Password is not correct");


        user.Password = PasswordHasher.Hash(changePasswordDto.NewPassword);
        await _unitOfWork.SaveChanges();

        return  Response<TokenBodyResponse>.GetAcceptedRequest("Password Changed !",null);
    }
    
    public async Task<Response<TokenBodyResponse>> ChangeUserDetails
        (ChangeUserDetailsDto changeUserDetails)
    {
        if (!Ulid.TryParse(changeUserDetails.Id, out var ulid))
        {
            return Response<TokenBodyResponse>.GetNotAcceptedRequest("Invalid user ID format");
        }
        
        var user = await _unitOfWork.UserRepository.Find(u => u.Id == ulid);
        
        if (user is null)
            return Response<TokenBodyResponse>.GetNotAcceptedRequest("User Id is not correct");


        user.Email = changeUserDetails.Email;
        user.Name = changeUserDetails.Name;
        user.Bio = changeUserDetails.Bio;
        user.PicturePath = changeUserDetails.PicturePath;
        
        await _unitOfWork.SaveChanges();

        return  Response<TokenBodyResponse>.GetAcceptedRequest("Details have been changed",null);
    }


}
