using SB.PayrollManagement.Application.Dtos;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface ICommissionEmployeeService : IBaseService<CommissionEmployeeDto, CreateCommissionEmployeeDto, UpdateCommissionEmployeeDto>
    {
    }
}
