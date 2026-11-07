using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using game_store_platform.Data.Entities;
using game_store_platform.Data.Enums;

namespace game_store_platform.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAdminUserAsync(GameStoreDBContext context)
        {
            if (await context.Users.AnyAsync(u => u.Role == UserRole.Admin))
            {
                return;
            }

            var adminUser = new User
            {
                Username = "SuperAdmin",
                Email = "admin@gamestore.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow,
                WalletBalance = 0
            };

            context.Users.Add(adminUser);
            await context.SaveChangesAsync();
        }
    }
}