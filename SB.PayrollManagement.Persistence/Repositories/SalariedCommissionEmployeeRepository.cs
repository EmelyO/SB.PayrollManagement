using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Domain.Entities;
using SB.PayrollManagement.Persistence.Base;
using SB.PayrollManagement.Persistence.Context;

namespace SB.PayrollManagement.Persistence.Repositories
{
    public class SalariedCommissionEmployeeRepository : BaseRepository<SalariedCommissionEmployees>, ISalariedCommissionEmployeeRepository
    {
        public SalariedCommissionEmployeeRepository(PayrollManagementContext context) : base(context)
        {
        }
    }
}
