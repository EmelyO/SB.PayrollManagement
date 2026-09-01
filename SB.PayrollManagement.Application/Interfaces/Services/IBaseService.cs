using SB.PayrollManagement.Domain.Base;

namespace SB.PayrollManagement.Application.Interfaces.Services
{
    public interface IBaseService<TDto, TCreateDto, TUpdateDto>
    {
        Task<OperationResult<TDto>> GetByIdAsync(int id);
        Task<OperationResult<List<TDto>>> GetAllAsync();
        Task<OperationResult<TDto>> CreateAsync(TCreateDto dto);
        Task<OperationResult<TDto>> UpdateAsync(int id, TUpdateDto dto);
        Task<OperationResult<TDto>> DeleteAsync(int id);
    }
}
