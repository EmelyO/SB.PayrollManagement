using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Domain.Entities;
using SB.PayrollManagement.Persistence.Base;
using SB.PayrollManagement.Persistence.Context;

namespace SB.PayrollManagement.Persistence.Repositories
{
    public class EmployeeRepository: BaseRepository<Employees>, IEmployeeRepository
    {
        public EmployeeRepository(PayrollManagementContext context): base(context)
        {
            
        }
    }
}
