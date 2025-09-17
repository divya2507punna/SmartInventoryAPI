using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartInventoryAPI.Helpers
{
    public class JwtTokenHelper
    {
        private readonly IConfiguration _config;

        public JwtTokenHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string username, string role)
        {
            // ---------------- CLAIMS ----------------
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),  // Subject
                new Claim(ClaimTypes.Name, username),              // User Identity
                new Claim(ClaimTypes.Role, role),                  // Role for [Authorize(Roles="...")]
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique ID
            };

            // ---------------- KEY ----------------
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // ---------------- EXPIRY ----------------
            var expiryMinutes = Convert.ToInt32(_config["Jwt:ExpiryMinutes"]);
            var expires = DateTime.UtcNow.AddMinutes(expiryMinutes);

            // ---------------- TOKEN ----------------
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
