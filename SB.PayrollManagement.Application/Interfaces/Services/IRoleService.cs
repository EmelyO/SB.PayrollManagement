using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Base;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<OperationResult<List<RoleDto>>> GetAllAsync();
        Task<OperationResult<RoleDto>> GetByIdAsync(int id);
    }
}
