using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SB.PayrollManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private const int TokenExpirationMinutes = 120;

        private readonly IUsersService _usersService;
        private readonly IConfiguration _configuration;
        public AuthService(IUsersService usersService, IConfiguration configuration)
        {
            _usersService = usersService;
            _configuration = configuration;
        }

        public async Task<AuthTokenResult?> GenerateTokenAsync(string username, string password)
        {
            var result = await _usersService.ValidateUserAsync(username, password);

            if (!result.IsSuccess || result.Data == null)
                return null;

            var user = result.Data!;

            var secretKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not configured");
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UsuarioID", user.UsuarioId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Usuario),
                new Claim(ClaimTypes.NameIdentifier, user.Usuario),
                new Claim(ClaimTypes.Role, user.NombreRol)
            };

            var expiresAtUtc = DateTime.UtcNow.AddMinutes(TokenExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                audience: audience,
                notBefore: DateTime.UtcNow,
                expires: expiresAtUtc,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthTokenResult(tokenString, expiresAtUtc);
        }
    }
}
