using MediatR;


namespace Application.Features.Dependent.GetAllDependent;

public record GetAllDependentQuery() : IRequest<GetAllDependentQueryResult>;
