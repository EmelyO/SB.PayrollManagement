namespace SB.PayrollManagement.Application.Dtos
{
    public record DepartmentDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
