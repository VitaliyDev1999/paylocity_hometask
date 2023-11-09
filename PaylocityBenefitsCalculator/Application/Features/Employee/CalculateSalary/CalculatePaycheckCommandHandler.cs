using Application.Abstraction;
using Application.Abstraction.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Employee.CalculateSalary;

public class CalculatePaycheckCommandHandler : IRequestHandler<CalculatePaycheckCommand, CalculatePaycheckCommandResult>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPaycheckCalculator _paycheckCalculator;
    private readonly IMapper _mapper;

    public CalculatePaycheckCommandHandler(IEmployeeRepository employeeRepository, IPaycheckCalculator paycheckCalculator, IMapper mapper)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _paycheckCalculator = paycheckCalculator ?? throw new ArgumentNullException(nameof(paycheckCalculator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CalculatePaycheckCommandResult> Handle(CalculatePaycheckCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId) ?? throw new Exception("Employee was not found.");

        var paycheck = await _paycheckCalculator.CalculatePaycheck(employee);

        return new CalculatePaycheckCommandResult()
        {
            Paycheck = paycheck
        };

    }
}
