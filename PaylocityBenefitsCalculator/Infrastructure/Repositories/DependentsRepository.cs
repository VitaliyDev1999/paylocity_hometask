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

    public async Task<Dependent> CreateAsync(Dependent dependent)
    {
        var result = await _dataContext.Dependent.AddAsync(dependent);
        await _dataContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<IEnumerable<Dependent>> GetAllAsync()
    {
        return await _dataContext.Dependent.ToListAsync();
    }

    public async Task<Dependent?> GetByIdAsync(int id)
    {
        return await _dataContext.Dependent.FirstOrDefaultAsync();
    }
}
