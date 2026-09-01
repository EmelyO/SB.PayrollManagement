using SB.PayrollManagement.Application.Dtos;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthTokenResult?> GenerateTokenAsync(string username, string password);
    }
}
