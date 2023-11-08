using Application.Entities;

namespace Application.Abstraction;

public interface IEmployeeRepository
{
    public Task<Employee?> GetById(int id);

    public Task<IEnumerable<Employee>> GetAll();
}
