
namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string?> GenerateTokenAsync(string username, string password);
    }
}
