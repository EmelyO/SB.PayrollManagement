
using SB.PayrollManagement.Domain.Base;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IUsersService
    {
        Task<OperationResult<>> ValidateUserAsync(string codigoEmpleado, string password);
    }
}
