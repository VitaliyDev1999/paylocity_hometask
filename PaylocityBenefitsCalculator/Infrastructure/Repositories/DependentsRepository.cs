using Application.Abstraction;
using Application.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DependentsRepository : IDependentsRepository
{
    private readonly AppDbContext _dataContext;

    public DependentsRepository(AppDbContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<Dependent>> GetAll()
    {
        return await _dataContext.Dependent.ToListAsync();
    }

    public async Task<Dependent?> GetById(int id)
    {
        return await _dataContext.Dependent.FirstOrDefaultAsync();
    }
}
