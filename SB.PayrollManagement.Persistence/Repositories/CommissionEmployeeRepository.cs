using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Domain.Entities;
using SB.PayrollManagement.Persistence.Base;
using SB.PayrollManagement.Persistence.Context;

namespace SB.PayrollManagement.Persistence.Repositories
{
    public class CommissionEmployeeRepository : BaseRepository<CommissionEmployees>, ICommissionEmployeeRepository
    {
        public CommissionEmployeeRepository(PayrollManagementContext context) : base(context)
        {
        }
    }
}
