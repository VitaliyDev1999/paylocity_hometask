using Application.Abstraction;
using Application.Entities;
using Application.Features.Dependent.CreateDependent;
using FluentValidation;

namespace Application.Behaviors;

public sealed class CreateDependentCommandValidator : AbstractValidator<CreateDependentCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateDependentCommandValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));

        RuleFor(x => x.EmployeeId).MustAsync( async (employeeId, cancellationToken) => await BeValidUserAsync(employeeId))
            .WithMessage("Employee was not found.");

        RuleFor(x => x).MustAsync(async (command, cancellationToken) => await BeValidRelationshipAsync(command))
            .WithMessage("An employee may only have 1 spouse or domestic partner (not both).");
    }

    private async Task<bool> BeValidUserAsync(int employeeId)
    {
        var result = await _employeeRepository.GetByIdAsync(employeeId);
        return result != null;
    }

    private async Task<bool> BeValidRelationshipAsync(CreateDependentCommand command)
    {
        var employee = _employeeRepository.GetByIdAsync(command.EmployeeId).GetAwaiter().GetResult();
        var relationship = command.Relationship;

        if (relationship == Relationship.Spouse && employee.Dependents.Any(d => d.Relationship == Relationship.DomesticPartner || d.Relationship == Relationship.Spouse))
        {
            return false;
        }
        else if (relationship == Relationship.DomesticPartner && employee.Dependents.Any(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner))
        {
            return false;
        }

        return relationship != Relationship.Spouse || relationship != Relationship.DomesticPartner;
    }
}
