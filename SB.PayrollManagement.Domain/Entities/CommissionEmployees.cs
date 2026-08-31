using System.ComponentModel.DataAnnotations;

namespace SB.PayrollManagement.Domain.Entities
{
    public class CommissionEmployees
    {
        public int EmployeeId { get; set; }

        public decimal GrossSales { get; set; }

        public decimal CommissionRate { get; set; }
    }
}
