﻿using Application.Abstraction;
using Application.Entities;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extention;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDb");
        }).SeedData();

        serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
        serviceCollection.AddScoped<IDependentsRepository, DependentsRepository>();

        return serviceCollection;
    }

    public static void SeedData(this IServiceCollection serviceCollection)
    {
        #region Employee Mock
        var employees = new List<Employee>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
        #endregion

        using var scope = serviceCollection.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
        context.Employees.AddRange(employees);
        context.SaveChanges();
    }
}
