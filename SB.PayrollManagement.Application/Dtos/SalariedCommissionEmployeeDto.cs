namespace SB.PayrollManagement.Application.Dtos
{
    public record SalariedCommissionEmployeeDto
    {
        public int EmployeeId { get; init; }
        public decimal CommissionRate { get; init; }
        public decimal BaseSalary { get; init; }
    }
}
