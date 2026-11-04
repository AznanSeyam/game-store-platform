using System.Text;
using hotel_reservation_api.Core.Interfaces;
using hotel_reservation_api.Core.Services;
using hotel_reservation_api.Data;
using hotel_reservation_api.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HotelReservationConnection");
builder.Services.AddDbContext<HotelReservationDBContext>(options => options.UseNpgsql(connectionString));

// Register AutoMapper
builder.Services.AddAutoMapper(mp =>
{
    mp.AddProfile<MappingProfile>();
});

// Register Services & Interfaces (Hotel, Country, Auth, Token)
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:TokenKey"] ?? "super_secret_key_here_at_least_32_bytes_long")),
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
        var context = services.GetRequiredService<HotelReservationDBContext>();
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