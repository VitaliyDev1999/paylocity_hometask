using Application.Entities;

namespace Application.Abstraction.Repositories;

public interface IEmployeeRepository
{
    public Task<Employee?> GetByIdAsync(int id);

    public Task<IEnumerable<Employee>> GetAllAsync();
}
