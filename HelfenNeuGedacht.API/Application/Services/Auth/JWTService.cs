using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HelfenNeuGedacht.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using HelfenNeuGedacht.API.Application.Services.Auth.Dto;

namespace HelfenNeuGedacht.API.Application.Services.Auth.AuthService
{
    public class JWTService(UserManager<ApplicationUser> userManager, IConfiguration configuration) : ITokenService
    {
        public async Task<TokenDTO> CreateTokenAsync(ApplicationUser user)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

            // OrganizationId hinzufügen, wenn vorhanden für Dashboard
            if (user.OrganizationId.HasValue)
            {
                authClaims.Add(new Claim("OrganizationId", user.OrganizationId.Value.ToString()));
            }

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var jwtSecret = configuration.GetSection("JwtSettings")["Secret"]
                ?? throw new InvalidOperationException("JWT Secret nicht in Konfiguration gefunden");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var expiration = DateTime.UtcNow.AddDays(15);

            var token = new JwtSecurityToken(
                expires: expiration,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new TokenDTO
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration,
                roles = userRoles,
                id = user.Id
            };
        }
    }
}
