using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_reservation_api.Core.DTOs.Auth;
using hotel_reservation_api.Core.Interfaces;
using hotel_reservation_api.Data;
using hotel_reservation_api.Data.Entities;

namespace hotel_reservation_api.Core.Services
{
    public class AuthService(HotelReservationDBContext context, ITokenService tokenService) : IAuthService
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
                Role = "Customer"
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = tokenService.CreateJWTToken(user),
                Role = user.Role
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
                Token = tokenService.CreateJWTToken(user),
                Role = user.Role
            };
        }
    }
}