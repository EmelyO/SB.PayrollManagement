using Microsoft.EntityFrameworkCore;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Domain.Entities;
using SB.PayrollManagement.Persistence.Base;
using SB.PayrollManagement.Persistence.Context;

namespace SB.PayrollManagement.Persistence.Repositories
{
    public class PayrollRecordRepository : BaseRepository<PayrollRecords>, IPayrollRecordRepository
    {
        public PayrollRecordRepository(PayrollManagementContext context) : base(context)
        {
        }

        public async Task<PayrollRecords?> GetLatestByEmployeeIdAsync(int employeeId)
        {
            return await _context.PayrollRecords
                .Where(p => p.EmployeeId == employeeId)
                .OrderByDescending(p => p.WeekEndDate)
                .FirstOrDefaultAsync();
        }
    }
}
