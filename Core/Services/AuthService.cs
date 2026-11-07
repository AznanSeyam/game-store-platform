using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using game_store_platform.Core.DTOs.Auth;
using game_store_platform.Core.Interfaces;
using game_store_platform.Data;
using game_store_platform.Data.Entities;
using game_store_platform.Data.Enums;

namespace game_store_platform.Core.Services
{
    public class AuthService(GameStoreDBContext context, ITokenService tokenService) : IAuthService
    {
        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            if (await context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return null;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Role = UserRole.Customer,
                CreatedAt = DateTime.UtcNow,
                WalletBalance = 5000
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = tokenService.GenerateToken(user),
                Role = user.Role.ToString(),
                WalletBalance = user.WalletBalance
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null) return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid) return null;

            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = tokenService.GenerateToken(user),
                Role = user.Role.ToString(),
                WalletBalance = user.WalletBalance
            };
        }
    }
}