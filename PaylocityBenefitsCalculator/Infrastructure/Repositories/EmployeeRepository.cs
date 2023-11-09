using Application.Abstraction.Repositories;
using Application.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly  AppDbContext _dataContext;

    public EmployeeRepository(AppDbContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _dataContext.Employees.Include(x => x.Dependents).ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _dataContext.Employees.Include(x => x.Dependents).FirstOrDefaultAsync(x => x.Id == id);
    }
}
