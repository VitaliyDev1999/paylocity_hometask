using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Dependent> Dependent { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
