using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_store_platform.Core.DTOs.Auth;

namespace game_store_platform.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}