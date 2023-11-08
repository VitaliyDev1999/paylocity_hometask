using Application.Entities;

namespace Application.Abstraction;

public interface IDependentsRepository
{
    public Task<Dependent?> GetById(int id);

    public Task<IEnumerable<Dependent>> GetAll();
}
