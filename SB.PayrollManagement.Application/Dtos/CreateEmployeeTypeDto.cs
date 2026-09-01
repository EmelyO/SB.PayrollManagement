namespace SB.PayrollManagement.Application.Dtos
{
    public record CreateEmployeeTypeDto
    {
        public string Name { get; init; } = string.Empty;
    }
}
