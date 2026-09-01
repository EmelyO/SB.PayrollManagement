using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Interfaces.Repositories
{
    public interface IPayrollRecordRepository : IBaseRepository<PayrollRecords>
    {
        Task<PayrollRecords?> GetLatestByEmployeeIdAsync(int employeeId);
    }
}
