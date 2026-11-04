using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_reservation_api.Data.Entities;

namespace hotel_reservation_api.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAdminUserAsync(HotelReservationDBContext context)
        {
            // চেক করা অলরেডি কোনো এডমিন আছে কি না
            if (await context.Users.AnyAsync(u => u.Role == "Admin"))
            {
                return;
            }

            var adminUser = new User
            {
                Username = "SuperAdmin",
                Email = "admin@hotel.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), // সিকিউর পাসওয়ার্ড
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };

            context.Users.Add(adminUser);
            await context.SaveChangesAsync();
        }
    }
}