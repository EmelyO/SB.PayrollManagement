namespace SB.PayrollManagement.Application.Dtos
{
    public record SalariedEmployeeDto
    {
        public int EmployeeId { get; init; }
        public decimal WeeklySalary { get; init; }
        public decimal WeeklyPay { get; init; }
    }
}
