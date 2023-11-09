using Application.Abstraction.Repositories;
using Application.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DependentsRepository : BaseRepository<Dependent>, IDependentsRepository
{
    private readonly AppDbContext _dataContext;

    public DependentsRepository(AppDbContext dataContext)
        :base(dataContext)
    {
        _dataContext = dataContext;
    }
}
