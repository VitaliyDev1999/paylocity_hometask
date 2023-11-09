using Application.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using Application.Entities;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly DbContext _dbContext;

    public BaseRepository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
        _dbContext = dbContext;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public virtual async Task Attach(TEntity entity)
    {
        _dbSet.Attach(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task AttachRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AttachRange(entities);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        await _dbContext.SaveChangesAsync();
    }
}
