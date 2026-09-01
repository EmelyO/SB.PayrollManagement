namespace SB.PayrollManagement.Application.Dtos
{
    public record CreateDepartmentDto
    {
        public string Name { get; init; } = string.Empty;
    }
}
