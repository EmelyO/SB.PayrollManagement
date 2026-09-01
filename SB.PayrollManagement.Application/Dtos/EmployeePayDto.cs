namespace SB.PayrollManagement.Application.Dtos
{
    public record EmployeePayDto
    {
        public int EmployeeId { get; init; }
        public string EmployeeType { get; init; } = string.Empty;
        public decimal WeeklyPay { get; init; }
    }
}
