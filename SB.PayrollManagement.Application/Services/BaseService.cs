using Microsoft.Extensions.Logging;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Domain.Base;

namespace SB.PayrollManagement.Application.Services
{
    public abstract class BaseService<TDto, TCreateDto, TUpdateDto, TEntity> : IBaseService<TDto, TCreateDto, TUpdateDto> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly ILogger<BaseService<TDto, TCreateDto, TUpdateDto, TEntity>> _logger;

        protected BaseService(IBaseRepository<TEntity> repository,
            ILogger<BaseService<TDto, TCreateDto, TUpdateDto, TEntity>> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        protected abstract TDto MapToDto(TEntity entity);
        protected abstract TEntity MapToEntity(TCreateDto dto);
        protected abstract void UpdateEntity(TUpdateDto dto, TEntity entity);

        public async Task<OperationResult<List<TDto>>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync(x => true);
                if (!result.IsSuccess || result.Data is null)
                {
                    return OperationResult<List<TDto>>.Failure(result.Message ?? "No elements found");
                }
                List<TEntity> entities = result.Data;
                var dtos = entities.Select(MapToDto).ToList();
                return OperationResult<List<TDto>>.Success("Data retrieved successfully", dtos);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo datos");
                return OperationResult<List<TDto>>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult<TDto>> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting entity of type {EntityType} with ID {Id}", typeof(TEntity).Name, id);
            try
            {
                if (id <= 0)
                {
                    return OperationResult<TDto>.Failure("The ID must be greater than 0");
                }
                var result = await _repository.GetByIdAsync(id);
                if (!result.IsSuccess || result.Data is null)
                {
                    return OperationResult<TDto>.Failure(result.Message ?? $"No entity found with ID {id}");
                }
                TEntity entity = result.Data;
                return OperationResult<TDto>.Success("Entity retrieved successfully", MapToDto(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the entity with ID {Id}", id);
                return OperationResult<TDto>.Failure($"Error retrieving entity: {ex.Message}");
            }

        }

        public async Task<OperationResult<TDto>> CreateAsync(TCreateDto dto)
        {
            try
            {
                if (dto is null)
                {
                    return OperationResult<TDto>.Failure("The entity to create cannot be null");
                }

                var entity = MapToEntity(dto);
                var result = await _repository.AddAsync(entity);
                if (!result.IsSuccess || result.Data is null)
                {
                    return OperationResult<TDto>.Failure(result.Message ?? "Error creating the entity");
                }

                TEntity created = result.Data;
                return OperationResult<TDto>.Success("Entity created successfully", MapToDto(created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an entity of type {EntityType}", typeof(TEntity).Name);
                return OperationResult<TDto>.Failure($"Error creating entity: {ex.Message}");
            }
        }

        public async Task<OperationResult<TDto>> UpdateAsync(int id, TUpdateDto dto)
        {
            try
            {
                if (id <= 0)
                {
                    return OperationResult<TDto>.Failure("The ID must be greater than 0");
                }
                if (dto is null)
                {
                    return OperationResult<TDto>.Failure("The entity to update cannot be null");
                }

                var existingResult = await _repository.GetByIdAsync(id);
                if (!existingResult.IsSuccess || existingResult.Data is null)
                {
                    return OperationResult<TDto>.Failure(existingResult.Message ?? $"No entity found with ID {id}");
                }

                TEntity existingEntity = existingResult.Data;
                UpdateEntity(dto, existingEntity);

                var result = await _repository.UpdateAsync(existingEntity);
                if (!result.IsSuccess || result.Data is null)
                {
                    return OperationResult<TDto>.Failure(result.Message ?? "Error updating the entity");
                }

                TEntity updated = result.Data;
                return OperationResult<TDto>.Success("Entity updated successfully", MapToDto(updated));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the entity with ID {Id}", id);
                return OperationResult<TDto>.Failure($"Error updating entity: {ex.Message}");
            }
        }

        public async Task<OperationResult<TDto>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return OperationResult<TDto>.Failure("The ID must be greater than 0");
                }

                var existingResult = await _repository.GetByIdAsync(id);
                if (!existingResult.IsSuccess || existingResult.Data is null)
                {
                    return OperationResult<TDto>.Failure(existingResult.Message ?? $"No entity found with ID {id}");
                }

                TEntity existingEntity = existingResult.Data;
                var dto = MapToDto(existingEntity);

                var result = await _repository.DeleteAsync(existingEntity);
                if (!result.IsSuccess)
                {
                    return OperationResult<TDto>.Failure(result.Message ?? "Error deleting the entity");
                }

                return OperationResult<TDto>.Success("Entity deleted successfully", dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the entity with ID {Id}", id);
                return OperationResult<TDto>.Failure($"Error deleting entity: {ex.Message}");
            }
        }
    }
}
