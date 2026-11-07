using System.Threading.Tasks;
using game_store_platform.Core.DTOs.Auth;
using game_store_platform.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace game_store_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await authService.RegisterAsync(registerDto);
            if (result == null) return BadRequest("Email already exists.");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await authService.LoginAsync(loginDto);
            if (result == null) return Unauthorized("Invalid email or password.");

            return Ok(result);
        }
    }
}