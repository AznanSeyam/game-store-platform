/*using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using hotel_reservation_api.Core.Interfaces;
using hotel_reservation_api.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace hotel_reservation_api.Core.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{

    private readonly SymmetricSecurityKey _key = new SymmetricSecurityKey(
     Encoding.UTF8.GetBytes(configuration["JWT:TokenKey"] ?? "a7F9kL2mPq4Rt8Vx1Yz3Bc5Df6Gh0Jk9Mn2Op4Qr7St8Uv1Wx3Yz5Ab6Cd7Ef8Gh9Ij0"));

    public string CreateJWTToken(User user)
    {
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(3),
            SigningCredentials = creds,
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

}
*/