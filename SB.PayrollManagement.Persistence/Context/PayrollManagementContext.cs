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

            modelBuilder.Entity<PayrollRecords>()
                .HasIndex(p => new { p.EmployeeId, p.WeekStartDate })
                .IsUnique();
        }

        public DbSet<Departments> Departments { get; set; }
        public DbSet<CommissionEmployees> CommissionEmployees { get; set; }

        public DbSet<Employees> Employees { get; set; }

        public DbSet<EmployeeTypes> EmployeeTypes { get; set; }

        public DbSet<SalariedCommissionEmployees> SalariedCommissionEmployees { get; set; }

        public DbSet<SalariedEmployees> SalariedEmployees { get; set; }

        public DbSet<GovernmentEntities> GovernmentEntities { get; set; }

        public DbSet<HourlyEmployees> HourlyEmployees { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<PayrollRecords> PayrollRecords { get; set; }

    }
}
