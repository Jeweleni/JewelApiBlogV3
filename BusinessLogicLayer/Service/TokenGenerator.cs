using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogicLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;



namespace BusinessLogicLayer.Service
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryInMinutes;

        public TokenGenerator(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            _key = jwtSettings["Secret"] ?? throw new ArgumentNullException("JWT Secret is missing.");
            _issuer = jwtSettings["Issuer"] ?? throw new ArgumentNullException("JWT Issuer is missing.");
            _audience = jwtSettings["Audience"] ?? throw new ArgumentNullException("JWT Audience is missing.");

            if (!int.TryParse(jwtSettings["ExpiryInMinutes"], out _expiryInMinutes))
            {
                _expiryInMinutes = 60; // Default to 60 minutes if not set
            }
        }

        public string GenerateToken(string userId, string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
