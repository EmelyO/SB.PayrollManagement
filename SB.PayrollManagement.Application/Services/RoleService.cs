using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Dtos;
using SB.PayrollManagement.Application.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Base;
using SB.PayrollManagement.Domain.Entities;

namespace SB.PayrollManagement.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IRolesRepository rolesRepository, ILogger<RoleService> logger)
        {
            _rolesRepository = rolesRepository;
            _logger = logger;
        }

        public async Task<OperationResult<List<RoleDto>>> GetAllAsync()
        {
            try
            {
                var result = await _rolesRepository.GetAllAsync(x => true);
                if (!result.IsSuccess || result.Data is null)
                {
                    return OperationResult<List<RoleDto>>.Failure(result.Message ?? "No elements found");
                }

                List<Roles> roles = result.Data;
                var dtos = roles.Select(r => r.ToDto()).ToList();
                return OperationResult<List<RoleDto>>.Success("Data retrieved successfully", dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles");
                return OperationResult<List<RoleDto>>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult<RoleDto>> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return OperationResult<RoleDto>.Failure("The ID must be greater than 0");
                }

                var result = await _rolesRepository.GetByIdAsync(id);
                if (!result.IsSuccess || result.Data is null)
                {
                    return OperationResult<RoleDto>.Failure(result.Message ?? $"No entity found with ID {id}");
                }

                Roles role = result.Data;
                return OperationResult<RoleDto>.Success("Entity retrieved successfully", role.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving role with ID {Id}", id);
                return OperationResult<RoleDto>.Failure($"Error retrieving entity: {ex.Message}");
            }
        }
    }
}
