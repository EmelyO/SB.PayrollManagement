namespace SB.PayrollManagement.Domain.Entities
{
    public class PayrollRecords
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateOnly WeekStartDate { get; set; }

        public DateOnly WeekEndDate { get; set; }

        public decimal? HoursWorked { get; set; }

        public decimal? GrossSales { get; set; }

        public decimal CalculatedPay { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
