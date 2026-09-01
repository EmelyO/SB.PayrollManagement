namespace SB.PayrollManagement.Application.Dtos
{
    public record CreateCommissionEmployeeDto
    {
        public int EmployeeId { get; init; }
        public decimal CommissionRate { get; init; }
    }
}
