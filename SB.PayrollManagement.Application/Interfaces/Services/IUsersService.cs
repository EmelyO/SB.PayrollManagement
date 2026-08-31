using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Domain.Base;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IUsersService : IBaseService<UserDto, CreateUserDto>
    {
        Task<OperationResult<UserAuthDto>> ValidateUserAsync(string username, string password);
    }
}
