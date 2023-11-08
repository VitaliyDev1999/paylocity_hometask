using Application.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Features.Employee.GetAllEmployee;

internal class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, GetAllEmployeeQueryResult>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetAllEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetAllEmployeeQueryResult> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        var result = await _employeeRepository.GetAll();

        return new GetAllEmployeeQueryResult()
        {
            EmployeeQueryResults = _mapper.Map<List<GetEmployeeQueryResult>>(result),
        };
    }
}
