

using SB.PayrollManagement.Domain.Base;
using System.Linq.Expressions;

namespace SB.PayrollManagement.Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult<List<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult<TEntity>> GetByIdAsync(int id);
        Task<OperationResult<TEntity>> AddAsync(TEntity entity);

        Task<OperationResult<TEntity>> UpdateAsync(TEntity entity);

        Task<OperationResult<TEntity>> DeleteAsync(TEntity entity);
    }
}
