using Microsoft.EntityFrameworkCore;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Persistence.Context
{
    public class PayrollManagementContext: DbContext
    {
        public PayrollManagementContext(DbContextOptions<PayrollManagementContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CommissionEmployees>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<SalariedEmployees>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<HourlyEmployees>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<SalariedCommissionEmployees>().HasKey(e => e.EmployeeId);
        }

        DbSet<Departments> Departments { get; set; }
        DbSet<CommissionEmployees> ComissionEmployees { get; set; }

        DbSet<Employees> Employees { get; set; }

        DbSet<EmployeeTypes> EmployeesTypes { get; set; }

        DbSet<SalariedCommissionEmployees> SalariedCommissionEmployees { get; set; }

        DbSet<SalariedEmployees> SalariedEmployeesTypes { get;set; }

        public DbSet<GovernmentEntities> GovernmentEntities { get; set; }

        DbSet<HourlyEmployees> HourlyEmployees { get;set; }

    }
}
