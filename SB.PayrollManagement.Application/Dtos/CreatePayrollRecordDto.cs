namespace SB.PayrollManagement.Application.Dtos
{
    public record CreatePayrollRecordDto
    {
        public int EmployeeId { get; init; }
        public DateOnly WeekStartDate { get; init; }
        public DateOnly WeekEndDate { get; init; }
        public decimal? HoursWorked { get; init; }
        public decimal? GrossSales { get; init; }
    }
}
