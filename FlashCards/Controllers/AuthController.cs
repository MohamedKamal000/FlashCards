using Application.Dtos.UserDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var result = await _authService.RegisterUser(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var result = await _authService.LoginUser(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var result = await _authService.ChangePassword(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("ChangeDetails")]
        public async Task<IActionResult> ChangeUserDetails([FromBody] ChangeUserDetailsDto dto)
        {
            var result = await _authService.ChangeUserDetails(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
