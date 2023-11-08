using MediatR;
namespace Application.Features.Employee.GetEmployeeById;

public record GetEmployeeByIdQuery : IRequest<GetEmployeeByIdQueryResult>
{
    public int Id { get; init; }
}
