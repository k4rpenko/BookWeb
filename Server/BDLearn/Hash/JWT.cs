using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BDLearn.Controllers
{
    internal class JWT
    {
        private readonly byte[] Key;

        public JWT()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            Key = Convert.FromBase64String(builder.GetSection("JWT:Key").Value);
        }
        public string GenerateJwtToken(Guid userId)
        {
            var securityKey = new SymmetricSecurityKey(Key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }

        public string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);

            return userIdClaim?.Value;
        }
    }
}
