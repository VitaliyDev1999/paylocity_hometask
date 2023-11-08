using Application.Entities;
using Application.Features.Employee;

namespace Application.Features.Dependent;

public class GetDependentQueryResult
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
    public GetEmployeeQueryResult Employee { get; set; }
}
