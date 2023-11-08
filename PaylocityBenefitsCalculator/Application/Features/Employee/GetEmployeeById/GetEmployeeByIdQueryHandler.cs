using Application.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Features.Employee.GetEmployeeById;

internal class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdQueryResult>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetEmployeeByIdQueryResult> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _employeeRepository.GetById(request.Id) ?? throw new Exception("Employee was not found.");

        return new GetEmployeeByIdQueryResult()
        {
            EmployeeQueryResult = _mapper.Map<GetEmployeeQueryResult>(response)
        };
    }
}

