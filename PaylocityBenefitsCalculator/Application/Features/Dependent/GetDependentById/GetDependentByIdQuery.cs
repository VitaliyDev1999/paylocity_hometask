using MediatR;

namespace Application.Features.Dependent.GetDependentById;

public class GetDependentByIdQuery : IRequest<GetDependentByIdQueryResult>
{
    public int Id { get; init; }
}
