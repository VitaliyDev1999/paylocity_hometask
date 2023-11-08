using Application.Abstraction;
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

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _dataContext.Employees.ToListAsync();
    }

    public async Task<Employee?> GetById(int id)
    {
        return await _dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
    }
}
