namespace Application.Abstraction.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task Attach(TEntity entity);
    Task AttachRange(IEnumerable<TEntity> entities);
    Task Delete(TEntity entity);
    Task DeleteRange(IEnumerable<TEntity> entities);
}
