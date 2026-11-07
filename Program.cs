using System.Text;
//using game_store_platform.Core.Interfaces;
//using game_store_platform.Core.Services;
using game_store_platform.Data;
using game_store_platform.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("GameStoreConnection");
builder.Services.AddDbContext<GameStoreDBContext>(options => options.UseNpgsql(connectionString));

// Register AutoMapper
builder.Services.AddAutoMapper(mp =>
{
    mp.AddProfile<MappingProfile>();
});

// Register Services & Interfaces (Hotel, Country, Auth, Token)
/*builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();*/

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:TokenKey"]!)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"]
        };
    });

builder.Services.AddControllers();

var app = builder.Build();

// Database Seeding (Admin User create korar jonno)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<GameStoreDBContext>();
        await context.Database.MigrateAsync();
        await DbInitializer.SeedAdminUserAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration or seeding.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Authentication ekhane dite hobe Authorization er AGE
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();