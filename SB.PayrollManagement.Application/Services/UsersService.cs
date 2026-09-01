using BCryptHasher = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Base;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class UsersService : BaseService<UserDto, CreateUserDto, CreateUserDto, Users>, IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IUsersRepository usersRepository,
            IRolesRepository rolesRepository,
            ILogger<BaseService<UserDto, CreateUserDto, CreateUserDto, Users>> baseLogger,
            ILogger<UsersService> logger)
            : base(usersRepository, baseLogger)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _logger = logger;
        }

        protected override UserDto MapToDto(Users entity) => entity.ToDto();
        protected override Users MapToEntity(CreateUserDto dto) => dto.ToEntity();
        protected override void UpdateEntity(CreateUserDto dto, Users entity) => dto.ApplyTo(entity);

        public async Task<OperationResult<UserAuthDto>> ValidateUserAsync(string username, string password)
        {
            try
            {
                var user = await _usersRepository.GetByUsernameAsync(username);

                if (user is null || !BCryptHasher.Verify(password, user.PasswordHash))
                {
                    return OperationResult<UserAuthDto>.Failure("Invalid credentials");
                }

                var roleResult = await _rolesRepository.GetByIdAsync(user.RoleId);
                var roleName = string.Empty;
                if (roleResult.IsSuccess && roleResult.Data is not null)
                {
                    Roles role = roleResult.Data;
                    roleName = role.Name;
                }

                var userDto = user.ToUsersDtoFromEntity(roleName);

                return OperationResult<UserAuthDto>.Success("User validated successfully", userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating user {Username}", username);
                return OperationResult<UserAuthDto>.Failure($"Error: {ex.Message}");
            }
        }
    }
}
