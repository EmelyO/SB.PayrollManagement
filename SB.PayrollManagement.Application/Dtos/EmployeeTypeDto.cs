namespace SB.PayrollManagement.Application.Dtos
{
    public record EmployeeTypeDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
