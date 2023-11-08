using Application.Entities;

namespace Application.Abstraction;

public interface IDependentsRepository
{
    public Task<Dependent?> GetByIdAsync(int id);

    public Task<IEnumerable<Dependent>> GetAllAsync();

    public Task<Dependent> CreateAsync(Dependent dependent);
}
