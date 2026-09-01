namespace SB.PayrollManagement.Application.Dtos
{
    public record CommissionEmployeeDto
    {
        public int EmployeeId { get; init; }
        public decimal CommissionRate { get; init; }
    }
}
