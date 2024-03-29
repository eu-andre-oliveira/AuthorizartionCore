using Application.Configurations.Options;
using Application.Services.Interfaces;
using Domain.Models.Authorizations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly AuthenticationBearerOptions _options;
        public TokenService(IOptions<AuthenticationBearerOptions> options)
        {
            _options = options.Value;
        }
        public string GenerateToken(LoginRequest loginRequest)
        {
            JwtSecurityTokenHandler handler = new();
            byte[] key = Encoding.UTF8.GetBytes(_options.PrivateKey);
            SigningCredentials credentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = GenerateClaims(loginRequest, ["operator"]),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(8),

            };
            var token = handler.CreateToken(tokenDescriptor);

            var strToken = handler.WriteToken(token);

            return strToken;
        }

        private ClaimsIdentity GenerateClaims(LoginRequest loginRequest, string[] roles)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, loginRequest.UserName));
            foreach (var role in roles)
                claims.AddClaim(new Claim(ClaimTypes.Role, role));

            return claims;
        }
    }
}
