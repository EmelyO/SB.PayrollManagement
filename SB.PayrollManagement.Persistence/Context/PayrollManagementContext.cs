using Microsoft.EntityFrameworkCore;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Persistence.Context
{
    public class PayrollManagementContext: DbContext
    {
        DbSet<Departments> Departments { get; set; }
        DbSet<CommissionEmployees> ComissionEmployees { get; set; }

        DbSet<Employees> Employees { get; set; }

        DbSet<EmployeeTypes> EmployeesTypes { get; set; }

        DbSet<SalariedCommissionEmployees> SalariedCommissionEmployees { get; set; }

        DbSet<SalariedEmployees> SalariedEmployeesTypes { get;set; }

        DbSet<GovernmentEntities> GovernmentEntities { get; set; }

        DbSet<HourlyEmployees> HourlyEmployees { get;set; }

    }
}
