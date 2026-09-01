using Microsoft.EntityFrameworkCore;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Domain.Base;
using SB.PayrollManagement.Persistence.Context;
using System.Linq.Expressions;

namespace SB.PayrollManagement.Persistence.Base
{
    public abstract class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity : class
    {
        public readonly PayrollManagementContext _context;
        public readonly DbSet<TEntity> _dbSet;
        public BaseRepository(PayrollManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<OperationResult<List<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {

                var data = await _dbSet.Where(filter).ToListAsync();
                return OperationResult<List<TEntity>>.Success($"Entities {typeof(TEntity)} retrieved successfully", data);
            }
            catch (Exception ex)
            {

                return OperationResult<List<TEntity>>.Failure($"Error retrieving entity: {typeof(TEntity)} - {ex.Message}");
            }
        }
        public virtual async Task<OperationResult<TEntity>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity is null)
                {
                    return OperationResult<TEntity>.Failure("The entity not found in the database");
                }
                return OperationResult<TEntity>.Success($"Entity {typeof(TEntity)} retrieved successfully", entity);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return OperationResult<TEntity>.Failure($"Error retrieving entity: {typeof(TEntity)} - {innerMessage}");
            }
        }
        public virtual async Task<OperationResult<TEntity>> AddAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return OperationResult<TEntity>.Success($"Entity {typeof(TEntity)} added successfully", entity);
            }
            catch (Exception ex)
            {

                var innerMessage = ex.InnerException?.Message ?? ex.Message;

                return OperationResult<TEntity>.Failure($"Error adding entity: {innerMessage}");
            }
        }
        public virtual async Task<OperationResult<TEntity>> UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return OperationResult<TEntity>.Success($"Entity {typeof(TEntity)} updated successfully", entity);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return OperationResult<TEntity>.Failure($"Error updating entity: {innerMessage}");
            }
        }
        public virtual async Task<OperationResult<TEntity>> DeleteAsync(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return OperationResult<TEntity>.Success($"Entity {typeof(TEntity)} delete successfully", entity);
            }
            catch (Exception ex)
            {
                return OperationResult<TEntity>.Failure($"Error updating entity: {typeof(TEntity)} - {ex.Message}");
            }
        }

        public virtual async Task<OperationResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var exists = await _dbSet.AnyAsync(filter);
                return OperationResult<bool>.Success($"Existence check on {typeof(TEntity)} completed successfully", exists);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return OperationResult<bool>.Failure($"Error checking existence for entity {typeof(TEntity)}: {innerMessage}");
            }
        }
    }
}
