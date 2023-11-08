using Application.Entities;
using Application.Features.Dependent;

namespace Application.Features.Employee;

public class GetEmployeeQueryResult
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<GetDependentQueryResult> Dependents { get; set; } = new List<GetDependentQueryResult>();
}
