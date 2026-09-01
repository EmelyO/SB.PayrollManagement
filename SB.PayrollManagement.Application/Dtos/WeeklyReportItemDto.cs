namespace SB.PayrollManagement.Application.Dtos
{
    public record WeeklyReportItemDto
    {
        public int EmployeeId { get; init; }
        public string EmployeeName { get; init; } = string.Empty;
        public string EmployeeType { get; init; } = string.Empty;
        public decimal? HoursWorked { get; init; }
        public decimal? GrossSales { get; init; }
        public decimal CalculatedPay { get; init; }
    }
}
