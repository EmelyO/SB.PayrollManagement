namespace SB.PayrollManagement.Application.Dtos
{
    public record UserDto
    {
        public int Id { get; init; }
        public string Username { get; init; } = string.Empty;
        public int RoleId { get; init; }
        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; init; }
    }
}
