using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_reservation_api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace hotel_reservation_api.Data;

public class HotelReservationDBContext(DbContextOptions<HotelReservationDBContext> options) : DbContext(options)
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<User> Users { get; set; }
}