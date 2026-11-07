using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_store_platform.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace game_store_platform.Data;

public class GameStoreDBContext(DbContextOptions<GameStoreDBContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Order> Orders { get; set; }
}