using MediatR;

namespace Application.Features.Employee.GetAllEmployee;

public record GetAllEmployeeQuery() : IRequest<GetAllEmployeeQueryResult>;
