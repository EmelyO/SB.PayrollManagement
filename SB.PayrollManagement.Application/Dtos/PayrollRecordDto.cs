namespace SB.PayrollManagement.Application.Dtos
{
    public record PayrollRecordDto
    {
        public int Id { get; init; }
        public int EmployeeId { get; init; }
        public DateOnly WeekStartDate { get; init; }
        public DateOnly WeekEndDate { get; init; }
        public decimal? HoursWorked { get; init; }
        public decimal? GrossSales { get; init; }
        public decimal CalculatedPay { get; init; }
        public DateTime CreatedDate { get; init; }
    }
}
