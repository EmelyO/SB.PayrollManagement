namespace SB.PayrollManagement.Application.Dtos
{
    public record UpdateSalariedCommissionEmployeeDto
    {
        public decimal CommissionRate { get; init; }
        public decimal BaseSalary { get; init; }
    }
}
