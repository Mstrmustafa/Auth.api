using Auth.common.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.common.TokenJwt
{
    public class TokenService(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(AppUser user) {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] // Defines the claims you want to include in the token. Claims are name/value pairs that contain information about the user and additional metadata.
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName), // 'Sub' (Subject) Claim which typically contains the user identifier.
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // 'Jti' (JWT ID) Claim provides a unique identifier for the token.
            new Claim(JwtRegisteredClaimNames.Email, user.Email), // A claim for the user's email.
            new Claim("role", "User") // Custom claim for the user role. You can add different or more claims as needed.
        };

            var token = new JwtSecurityToken(jwtSettings["Issuer"], jwtSettings["Audience"], claims, expires: DateTime.Now.AddHours(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
