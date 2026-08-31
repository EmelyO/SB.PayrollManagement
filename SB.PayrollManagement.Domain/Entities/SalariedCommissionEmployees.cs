

namespace SB.PayrollManagement.Domain.Entities
{
    public class SalariedCommissionEmployees
    {
        public int EmployeeId { get; set; }

        public decimal GrossSales { get; set; }

        public decimal CommissionRate { get; set; }

        public decimal BaseSalary { get; set; }
    }
}
