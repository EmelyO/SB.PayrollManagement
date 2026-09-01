namespace SB.PayrollManagement.Application.Dtos
{
    public record CreateSalariedEmployeeDto
    {
        public int EmployeeId { get; init; }
        public decimal WeeklySalary { get; init; }
    }
}
