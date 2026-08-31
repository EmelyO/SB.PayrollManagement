using System.ComponentModel.DataAnnotations;
namespace SB.PayrollManagement.Domain.Entities
{
    public class Employees
    {
        public int Id { get; set; }

        public int EmployeeTypeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SocialSecurityNumber { get; set; }

        public int? DepartmentId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
