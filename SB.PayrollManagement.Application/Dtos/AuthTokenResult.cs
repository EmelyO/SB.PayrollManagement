namespace SB.PayrollManagement.Application.Dtos
{
    public record AuthTokenResult(string Token, DateTime ExpiresAtUtc);
}
