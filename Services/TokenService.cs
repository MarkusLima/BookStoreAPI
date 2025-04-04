using BookStoreAPI.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }
        public string GenerateToken(User user)
        {

            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var secretKey = _configuration["JwtSettings:SecretKey"];


            var handler = new JwtSecurityTokenHandler();

            var credentials = new SigningCredentials(
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                   SecurityAlgorithms.HmacSha256Signature
               );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credentials
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);


        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Name, user.email));
            return ci;
        }
    }
}
