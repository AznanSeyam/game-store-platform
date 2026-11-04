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
    public class AuthService : IAuthService
    {
        private readonly HotelReservationDBContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(HotelReservationDBContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            // ইমেইল অলরেডি এক্সিস্ট করে কি না চেক করা
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return null;
            }

            // পাসওয়ার্ড হ্যাশ করা (সঠিক নিয়ম)
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Role = "Customer" // ডিফল্ট রোল কাস্টমার
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null) return null;

            // পাসওয়ার্ড ভেরিফাই করা (সঠিক নিয়ম)
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid) return null;

            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Role = user.Role
            };
        }
    }
}