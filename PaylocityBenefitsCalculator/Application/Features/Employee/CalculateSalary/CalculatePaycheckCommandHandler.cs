using Application.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Features.Employee.CalculateSalary;

public class CalculatePaycheckCommandHandler : IRequestHandler<CalculatePaycheckCommand, CalculatePaycheckCommandResult>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public CalculatePaycheckCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    Task<CalculatePaycheckCommandResult> IRequestHandler<CalculatePaycheckCommand, CalculatePaycheckCommandResult>.Handle(CalculatePaycheckCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
