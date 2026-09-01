using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Interfaces.Services;
using System.Security.Claims;

namespace SB.PayrollManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var authResult = await _authService.GenerateTokenAsync(dto.Username, dto.Password);
            if (authResult is null)
            {
                return Unauthorized(new { success = false, message = "Invalid credentials" });
            }

            Response.Cookies.Append("access_token", authResult.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = authResult.ExpiresAtUtc
            });

            return Ok(new { success = true });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            return Ok(new { success = true });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var userId = User.FindFirstValue("UsuarioID");
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new { userId, username, role });
        }
    }
}
