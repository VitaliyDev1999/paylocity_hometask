using Application.Abstraction.Repositories;
using Application.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    private readonly AppDbContext _dataContext;

    public EmployeeRepository(AppDbContext dataContext)
        : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public override async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _dataContext.Employees.Include(x => x.Dependents).ToListAsync();
    }
}
