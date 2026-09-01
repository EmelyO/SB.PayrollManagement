namespace SB.PayrollManagement.Application.Dtos
{
    public record UpdateCommissionEmployeeDto
    {
        public decimal CommissionRate { get; init; }
    }
}
