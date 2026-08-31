namespace SB.PayrollManagement.Application.Dtos
{
    public record CreateUserDto
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public int RoleId { get; init; }
    }
}
