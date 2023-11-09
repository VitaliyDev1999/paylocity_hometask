using MediatR;

namespace Application.Features.Employee.CalculateSalary;

public class CalculatePaycheckCommand : IRequest<CalculatePaycheckCommandResult>
{
    public int EmployeeId { get; init; }
}
