using Application.Entities;
using MediatR;

namespace Application.Features.Dependent.CreateDependent;

public class CreateDependentCommand : IRequest<CreateDependentCommandResult>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
}
