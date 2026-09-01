namespace SB.PayrollManagement.Application.Dtos
{
    public record UpdateSalariedEmployeeDto
    {
        public decimal WeeklySalary { get; init; }
    }
}
